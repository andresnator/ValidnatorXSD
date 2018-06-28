using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD.BL
{
    public class ValidateFileToXsd
    {
        private readonly IConfig _config;
        private readonly List<ResponseErrorsModel> _errors;
        private int _contador;
        private XmlReader _reader;


        public ValidateFileToXsd(IConfig config)
        {
            _errors = new List<ResponseErrorsModel>();
            _config = config;
        }

        public List<ResponseErrorsModel> GetErrorsFromXmlFile(XElement dataFile)
        {
            var myschema = XmlSchema.Read(_config.ShemaReader, null);


            var booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add(myschema);
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += BooksSettingsValidationEventHandler;

            Stream stream = new MemoryStream();
            dataFile.Save(stream);
            stream.Position = 0;

            _reader = XmlReader.Create(stream, booksSettings);

            while (_reader.Read())
            {
                if (!_reader.NodeType.Equals(XmlNodeType.Element)) continue;
                if (_reader.Name == ComunConst.Row) _contador++;
            }

            _reader.Close();


            return _errors;
        }


        private void BooksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            var mensaje = string.Empty;
            if (e.Exception.InnerException != null)
            {
                mensaje = e.Exception.InnerException.Message;
                if (mensaje == string.Empty) mensaje = e.Message;
            }
            else
            {
                if (mensaje == string.Empty) mensaje = e.Message;
            }

            if (_reader.Name.Contains(ComunConst.Column))
                _errors.Add(new ResponseErrorsModel
                {
                    ColumnPos = Convert.ToInt32(_reader.Name.Replace(ComunConst.Column, "")),
                    RowPos = _contador,
                    Message = mensaje
                });


            if (_reader.Name.Contains(ComunConst.Row))
                _errors.Add(new ResponseErrorsModel
                {
                    RowPos = _contador,
                    Message = mensaje
                });
        }

        public bool ValidQuantityColumns(List<DataFileModel> dataFileModels)
        {
            var quantityColumns = _config.QuantityColumns;
            return dataFileModels.All(c => c.QuantityColumns == quantityColumns);
        }

        public List<ResponseErrorsModel> GetColumnsErrorQuantity(List<DataFileModel> dataFileModels)
        {
            var quantityColumns = _config.QuantityColumns;


            return dataFileModels.Where(c => c.QuantityColumns != quantityColumns).Select(r => new ResponseErrorsModel
            {
                RowPos = r.RowNumber,
                Message = string.Format(Messages.FileErrorQuantityColumns, r.RowNumber.ToString(),
                    r.QuantityColumns.ToString()),
            }).ToList();
        }

        public bool ValidQuantityRows(List<DataFileModel> dataFileModels)
        {
            var quantityRows = _config.QuantityRows;
            return !dataFileModels.Any(r => r.RowNumber > quantityRows);
        }

        public ResponseErrorsModel GetRowsErrorQuantity(List<DataFileModel> dataFileModels)
        {
            var countRows = dataFileModels.Count;

            return new ResponseErrorsModel
            {
                Message = string.Format(Messages.FileErrorQuantityRows, _config.PathFile, countRows)
            };
        }
    }
}