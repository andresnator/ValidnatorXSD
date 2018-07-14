using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD.BL
{
    public class DataFile
    {
        private readonly IConfig _fileFeature;

        public DataFile(IConfig fileFeature)
        {
            _fileFeature = fileFeature;
        }

        public List<DataFileModel> GetDataFileModelList()
        {
            if (!File.Exists(_fileFeature.PathFile))
                throw new ArgumentException(string.Format(Messages.FileNotExist, _fileFeature.PathFile));

            var file = File.ReadAllLines(Path.Combine(_fileFeature.PathFile), Encoding.UTF8);


            int counterRow = 1;
            int counterCol;
            var separador = (char)_fileFeature.SeparatorColumn;

            
            var length = file.Count() / ComunConst.PaginateSize;
            var pageSize = ComunConst.PaginateSize;

            List<DataFileModel> dataFile = new List<DataFileModel>();
            for (int page = 0; page <= length; page++)
            {
                var dataFilePaginate = (from fileAux in file.Skip((page) * pageSize).Take(pageSize)
                 select new DataFileModel
                 {
                     RowNumber = counterRow++,
                     QuantityColumns = fileAux.Split(separador).Length,
                     DataRow = fileAux,
                     InitCounterCol = counterCol = 1,
                     ItemsRow = (from fileAuxIn in fileAux.Split(separador).ToList()
                                 select new RowDataFileModel
                                 {
                                     ValueCol = fileAuxIn,
                                     CounterCol = counterCol++
                                 }).ToList()
                 }).ToList();

                dataFile.AddRange(dataFilePaginate);
            }


            return dataFile;
        }


        public XElement GetXElementFromData(List<DataFileModel> dataFile)
        {
            var resultXml = new XElement(ComunConst.Table,
                from item in dataFile
                select new XElement(ComunConst.Row, from ir in item.ItemsRow
                                                    select new XElement(ComunConst.Column + ir.CounterCol, ir.ValueCol)));

            return resultXml;
        }
    }
}