using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD.BL
{
    public class GetDataFile
    {
        public List<DataFileModel> GetDataFileModel(IConfig fileFeature)
        {
            if (!File.Exists(fileFeature.PathFile))
                throw new ArgumentException(string.Format(Messages.FileNotExist, fileFeature.PathFile));

            var file = File.ReadAllLines(Path.Combine(fileFeature.PathFile), Encoding.UTF8);
            long counterRow = 1;
            long counterCol;
            var separador = (char) fileFeature.SeparatorColumn;


            var dataFile = (from fileAux in file //.Take(200000)
                select new DataFileModel
                {
                    RowNumber = counterRow++,
                    CantColumns = fileAux.Split(separador).Length,
                    //DataRow = fileAux,
                    DataRow = string.Empty,
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
    }
}