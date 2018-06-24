using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
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
        private readonly List<Resultados> _miRespuesta;
        private long _contador;
        private XmlReader _reader;


        public ValidateFileToXsd()
        {
            _miRespuesta = new List<Resultados>();
        }

        public List<Resultados> ValidXml(IConfig config, XElement dataFile)
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

            var resultado = new JavaScriptSerializer().Serialize(_miRespuesta.Take(20));

            return _miRespuesta;
        }


        private void BooksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            var mensaje = string.Empty;
            if (e.Exception.InnerException != null)
            {
                mensaje = e.Message;
                if (e.Message == string.Empty) mensaje = e.Exception.InnerException.Message;
            }


            _miRespuesta.Add(new Resultados
            {
                ColumnPos = Convert.ToInt64(_reader.Name.Replace(ComunConst.Column, "")),
                RowPos = _contador,
                Message = mensaje
            });
        }
    }
}