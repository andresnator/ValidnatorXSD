using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly List<ResponseErrorsModel> _miRespuesta;
        private long _contador;
        private XmlReader _reader;


        public ValidateFileToXsd()
        {
            _miRespuesta = new List<ResponseErrorsModel>();
        }

        public List<ResponseErrorsModel> ValidXml(IConfig config, XElement dataFile)
        {
            var myschema = XmlSchema.Read(config.ShemaReader, null);


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


            return _miRespuesta;
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
                _miRespuesta.Add(new ResponseErrorsModel
                {
                    ColumnPos = Convert.ToInt64(_reader.Name.Replace(ComunConst.Column, "")),
                    RowPos = _contador,
                    Message = mensaje
                });


            if (_reader.Name.Contains(ComunConst.Row))
                _miRespuesta.Add(new ResponseErrorsModel
                {
                    RowPos = _contador,
                    Message = mensaje
                });
        }
    }
}