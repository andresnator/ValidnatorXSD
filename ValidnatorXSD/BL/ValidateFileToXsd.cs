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
        private readonly Dictionary<string, string> _diccionarioData;
        private readonly List<ResponseErrorsModel> _errors;
        private int _contador;
        private XmlReader _reader;


        public ValidateFileToXsd(IConfig config)
        {
            _errors = new List<ResponseErrorsModel>();
            _config = config;

            _diccionarioData = new Dictionary<string, string>();
        }

        public List<ResponseErrorsModel> GetErrorsFromXmlFile(XElement dataFile)
        {
            XmlSchema myschema = XmlSchema.Read(_config.ShemaReader, null);


            SetDiccionaryValidate(myschema);


            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add(myschema);
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += BooksSettingsValidationEventHandler;

            Stream stream = new MemoryStream();
            dataFile.Save(stream);
            stream.Position = 0;

            _reader = XmlReader.Create(stream, booksSettings);

            while (_reader.Read())
            {
                if (!_reader.NodeType.Equals(XmlNodeType.Element))
                {
                    continue;
                }

                if (_reader.Name == ComunConst.Row)
                {
                    _contador++;
                }
            }

            _reader.Close();

            return _errors;
        }

        private void SetDiccionaryValidate(XmlSchema myschema)
        {
            foreach (XmlSchemaObject shemaO in myschema.Items)
            {
                if (shemaO.GetType().Name != "XmlSchemaSimpleType")
                {
                    continue;
                }


                foreach (XmlSchemaObject shema1 in ((XmlSchemaSimpleType) shemaO).Annotation.Items)
                {
                    if (shema1.GetType().Name != "XmlSchemaDocumentation")
                    {
                        continue;
                    }

                    XmlSchemaDocumentation documentacion = (XmlSchemaDocumentation) shema1;
                    if (documentacion.Source != null && documentacion.Markup[0].Value != null)
                    {
                        _diccionarioData.Add(documentacion.Source, documentacion.Markup[0].Value);
                    }
                }
            }
        }


        private void BooksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity != XmlSeverityType.Error)
            {
                return;
            }


            string mensaje = (from dic in _diccionarioData
                where dic.Key == _reader.Name
                select dic.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(mensaje))
            {
                mensaje = e.Message;
            }


            if (_reader.Name.Contains(ComunConst.Column))
            {
                _errors.Add(new ResponseErrorsModel
                {
                    ColumnPos = Convert.ToInt32(_reader.Name.Replace(ComunConst.Column, "")),
                    RowPos = _contador,
                    Message = mensaje
                });
            }


            if (_reader.Name.Contains(ComunConst.Row))
            {
                _errors.Add(new ResponseErrorsModel
                {
                    RowPos = _contador,
                    Message = mensaje
                });
            }
        }

        public bool ValidQuantityColumns(List<DataFileModel> dataFileModels)
        {
            int quantityColumns = _config.QuantityColumns;
            return dataFileModels.All(c => c.QuantityColumns == quantityColumns);
        }

        public List<ResponseErrorsModel> GetColumnsErrorQuantity(List<DataFileModel> dataFileModels)
        {
            int quantityColumns = _config.QuantityColumns;


            return dataFileModels.Where(c => c.QuantityColumns != quantityColumns).Select(r => new ResponseErrorsModel
            {
                RowPos = r.RowNumber,
                Message = string.Format(Messages.FileErrorQuantityColumns, r.RowNumber.ToString(),
                    r.QuantityColumns.ToString())
            }).ToList();
        }

        public bool ValidQuantityRows(List<DataFileModel> dataFileModels)
        {
            int? quantityRows = _config.QuantityRows;
            return !dataFileModels.Any(r => r.RowNumber > quantityRows);
        }

        public ResponseErrorsModel GetRowsErrorQuantity(List<DataFileModel> dataFileModels)
        {
            int countRows = dataFileModels.Count;

            return new ResponseErrorsModel
            {
                Message = string.Format(Messages.FileErrorQuantityRows, _config.PathFile, countRows)
            };
        }
    }
}