using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ClosedXML.Excel;
using CsvHelper;
using ValidnatorXSD.BL;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD
{

    

    public class ValidnatorXsd
    {
        private readonly IConfig _config;

        public ValidnatorXsd(IConfig config)
        {
            _config = config;
        }


        public List<ResponseErrorsModel> Start()
        {
            if (_config.ShemaReader == null)
            {
                throw new ArgumentException("Not Found 'ShemaReader' ");
            }

            List<ResponseErrorsModel> responseErrors = new List<ResponseErrorsModel>();
            List<DataFileModel> data = new DataFile(_config).GetDataFileModelList();
            bool validRowsQuantity = new ValidateFileToXsd(_config).ValidQuantityRows(data);
            bool validQuantityColumns = new ValidateFileToXsd(_config).ValidQuantityColumns(data);

            XElement xmElementFromData = new DataFile(_config).GetXElementFromData(data);

            List<ResponseErrorsModel> errorsValidate =
                new ValidateFileToXsd(_config).GetErrorsFromXmlFile(xmElementFromData);

            responseErrors.AddRange(errorsValidate);

            if (!validRowsQuantity)
            {
                responseErrors.Add(new ValidateFileToXsd(_config).GetRowsErrorQuantity(data));
            }

            if (!validQuantityColumns)
            {
                responseErrors.AddRange(new ValidateFileToXsd(_config).GetColumnsErrorQuantity(data));
            }

            return responseErrors;
        }

        public XLWorkbook StartExcel()
        {
            XLWorkbook wb = new XLWorkbook();
            IXLWorksheet ws = wb.Worksheets.Add(ComunConst.ErrorSheet);



            List<ResponseErrorsModel> dataError = new List<ResponseErrorsModel>();
            List<DataFileModel> data = new DataFile(_config).GetDataFileModelList();
            bool validRowsQuantity = new ValidateFileToXsd(_config).ValidQuantityRows(data);
            bool validQuantityColumns = new ValidateFileToXsd(_config).ValidQuantityColumns(data);

            XElement xmElementFromData = new DataFile(_config).GetXElementFromData(data);

            List<ResponseErrorsModel> errorsValidate =
                new ValidateFileToXsd(_config).GetErrorsFromXmlFile(xmElementFromData);

            dataError.AddRange(errorsValidate);




            data.ToList().ForEach(r =>
            {
                List<ResponseErrorsModel> errorFile =
                    dataError.Where(x => x.RowPos == r.RowNumber).ToList();




                r.ItemsRow.ForEach(c =>
                {
                    ws.Cell(r.RowNumber, c.CounterCol).SetValue(c.ValueCol);

                    if (!errorFile.Any())
                    {
                        return;
                    }

                    errorFile.Where(d => d.ColumnPos == null && !string.IsNullOrEmpty(d.Message)).ToList()
                        .ForEach(e =>
                        {
                            ws.Cell(r.RowNumber, c.CounterCol).Comment.SetAuthor(Messages.ErrorComment).AddSignature()
                                .AddText(e.Message).AddNewLine();
                            ws.Cell(r.RowNumber, c.CounterCol).Comment.Style.Size.SetAutomaticSize();
                            ws.Cell(r.RowNumber, c.CounterCol).Style.Fill.BackgroundColor = XLColor.Cyan;
                        });

                    errorFile.Where(d => d.ColumnPos == c.CounterCol && !string.IsNullOrEmpty(d.Message)).ToList()
                        .ForEach(e =>
                        {
                            ws.Cell(r.RowNumber, c.CounterCol).Comment.SetAuthor(Messages.ErrorComment).AddSignature()
                                .AddText(e.Message).AddNewLine();
                            ws.Cell(r.RowNumber, c.CounterCol).Comment.Style.Size.SetAutomaticSize();
                            ws.Cell(r.RowNumber, c.CounterCol).Style.Fill.BackgroundColor = XLColor.Yellow;
                        });
                });
            });

            ws.Columns().AdjustToContents();
            ws.Rows().AdjustToContents();

            return wb;
        }


        public string StartCsv()
        {
            var errorsValidate = Start();

            using (var strem = new StreamWriter(_config.PathFileCsv) )
            {

                var csv = new CsvWriter(strem);
                csv.WriteRecords(errorsValidate);

            }
            return _config.PathFileCsv;
        }
    }
}