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
        public List<DataFileModel> GetDataFileModel(IConfig fileFeature)
        {
            if (!File.Exists(fileFeature.PathFile))
                throw new ArgumentException(string.Format(Messages.FileNotExist, fileFeature.PathFile));

            var file = File.ReadAllLines(Path.Combine(fileFeature.PathFile), Encoding.UTF8);
            long counterRow = 1;
            long counterCol;
            var separador = (char) fileFeature.SeparatorColumn;

            //todo refactor .Take
            var dataFile = (from fileAux in file.Take(20000)
                select new DataFileModel
                {
                    RowNumber = counterRow++,
                    CantColumns = fileAux.Split(separador).Length,
                    DataRow = fileAux,
                    InitCounterCol = counterCol = 1,
                    ItemsRow = (from fileAuxIn in fileAux.Split(separador).ToList()
                        select new RowDataFileModel
                        {
                            ValueCol = fileAuxIn,
                            CounterCol = counterCol++
                        }).ToList()
                }).ToList();
            return dataFile;
        }


        public XElement ConvertFileXml(IConfig fileFeature, List<DataFileModel> dataFile)
        {
            var resultXml = new XElement(ComunConst.Table,
                from item in dataFile
                select new XElement(ComunConst.Row, from ir in item.ItemsRow
                    select new XElement(ComunConst.Column + ir.CounterCol, ir.ValueCol)));

            return resultXml;
        }
    }
}