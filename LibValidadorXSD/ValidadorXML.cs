using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using LibData.Models;

namespace LibValidadorXSD
{
    public class ValidadorXml
    {
        private readonly ArchivosProgramados _archivoProgramado;
        private long _contador;

        public List<Resultados> _miRespuesta = new List<Resultados>();
        private XmlReader _reader;
        public string ValCampo = string.Empty;

        public ValidadorXml(ArchivosProgramados archivoProgramado)
        {
            _archivoProgramado = archivoProgramado;
        }


        /// <summary>
        ///     Metodo que retorna una lista de los errores que se generarón al comparar un esquema con una fuente de datos
        /// </summary>
        /// <param name="stringXmlDatos">Datos en formato xml</param>
        /// <param name="schemaDinamico">Esquema contra el que se va a constrastar los datos</param>
        /// <returns></returns>
        public List<Resultados> ValidarXml(string stringXmlDatos, XmlSchemaSet schemaDinamico)
        {
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemaDinamico
            };

            settings.ValidationEventHandler += CallBackValidacion;
            _reader = XmlReader.Create(new StringReader(stringXmlDatos), settings);

            try
            {
                while (_reader.Read())
                {
                    //Inicio con vacio la variable
                    ValCampo = string.Empty;


                    if (_reader.NodeType.Equals(XmlNodeType.Text)) ValCampo = _reader.Value;

                    if (!_reader.NodeType.Equals(XmlNodeType.Element)) continue;

                    if (_reader.Name == "item") _contador++;
                }

                _reader.Close();
            }
            catch (XmlSchemaException ex)
            {
                //_miRespuesta.Add(
                //    $"El valor enviado '{ValCampo}' no es valido para la columna '{_reader.Name.Replace("columna", "")}' , y en la fila {_contador}");

                _miRespuesta.Add(new Resultados
                {
                    ArchivosProgramadosId = _archivoProgramado.ArchivosProgramadosId,
                    Columna = Convert.ToInt64(_reader.Name.Replace("columna", "")) + 1,
                    Fila = _contador,
                    Mensaje = ex.Message
                });
            }

            // retornar los errores en una cadena
            return _miRespuesta;
        }

        private void CallBackValidacion(object sender, ValidationEventArgs e)
        {
            var mensaje = "";
            if (e.Exception.InnerException != null)
            {
                mensaje = e.Message;
                if (e.Message == string.Empty) mensaje = e.Exception.InnerException.Message;
            }

            //_miRespuesta.Add(
            //    $"El valor enviado '{ValCampo}' no es valido para la columna '{(Convert.ToInt64(_reader.Name.Replace("columna", "")) + 1)}' , y en la fila {_contador}  ({mensaje})");

            _miRespuesta.Add(new Resultados
            {
                ArchivosProgramadosId = _archivoProgramado.ArchivosProgramadosId,
                Columna = Convert.ToInt64(_reader.Name.Replace("columna", "")) + 1,
                Fila = _contador,
                Mensaje = mensaje
            });
        }
    }
}