using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using LibData.Const;
using LibData.Models;

namespace LibValidadorXSD
{
    public class Program
    {
        private static readonly Program Instance = null;
        private ContextValidadorBd _context;
        private readonly List<Resultados> _resultadosList = new List<Resultados>();
        public static Program ProgramSingleton => Instance ?? new Program();


        public void Iniciar(long archivoProgramadoId)
        {
            //if (_context == null)
            _context = new ContextValidadorBd();


            ValidarProgramados(ActualizarEstadosEjecucion(ObtenerArchivosProgramados(archivoProgramadoId),
                Models.EEstadosArchivos.EnEjecucion));
        }


        /// <summary>
        ///     Metodo que convierte un archivo programado en un documento xml
        /// </summary>
        /// <param name="archivoProgramado">archivo programados</param>
        /// <returns>El archivo programado en formato XML</returns>
        private string ObtenerArchivoXml(ArchivosProgramados archivoProgramado)
        {
            string[] data = File
                .ReadAllLines(Path.
                    Combine(archivoProgramado.UrlArchivoCargado),
                    Encoding.UTF8); // datos leidos del archivo
            //var data = File.ReadAllLines(archivoProgramado.UrlArchivoCargado);// datos leidos del archivo


            //Path.Combine(Server.MapPath("~/Archivos/Programados"), nombre);
            long contador = 1;
            long contador2;
            char separador = Convert.ToChar(archivoProgramado.Archivo.Separador); // Separador del archivo  plano
            int cantidadColumnasHabilitadas =
                archivoProgramado.Archivo.CantidadColumnasHabilitadas; // cantidad de columnas habilitadas


            var datosArchivo = (from aux in data
                select new
                {
                    fila = contador++,
                    cantColumnas = aux.Split(separador).ToList().Count,
                    datosString = aux,
                    initContador = contador2 = 0,
                    datos = (from aaa in aux.Split(separador).ToList()
                        select new
                        {
                            valor = aaa,
                            cont = contador2++
                        }).ToList()
                }).ToList();


            var datosCantidadErr = datosArchivo.Where(x => x.cantColumnas != cantidadColumnasHabilitadas).ToList();
            var datosCantidadOk = datosArchivo.Where(x => x.cantColumnas == cantidadColumnasHabilitadas).ToList();


            foreach (var r in datosCantidadErr)
                _resultadosList.Add(new Resultados
                {
                    ArchivosProgramadosId = archivoProgramado.ArchivosProgramadosId,
                    Columna = r.initContador,
                    Fila = r.fila,
                    Mensaje = "Cantidad Columnas: " + r.cantColumnas + " fila:" + r.fila + " datos: " + r.datosString
                });

            if (!datosCantidadOk.Any())
            {
                return "";
            }

            XElement linqDinamico = new XElement("clase",
                from item in datosCantidadOk
                select new XElement("item", from r in item.datos
                    select new XElement("columna" + r.cont, r.valor)));

            return linqDinamico.ToString();
        }


        /// <summary>
        ///     Obtiene un archivo programado
        /// </summary>
        /// <param name="archivoProgramadoId">Archivo Programado</param>
        /// <returns>Archivo Programado</returns>
        private ArchivosProgramados ObtenerArchivosProgramados(long archivoProgramadoId)
        {
            return
                _context
                    .ArchivosProgramados
                    .Include(c => c.Archivo)
                    .Include(c => c.Archivo.Columna)
                    .Where(c => c.EstadosArchivos == Models.EEstadosArchivos.Programado)
                    .SingleOrDefault(c => c.ArchivosProgramadosId == archivoProgramadoId);
        }

        /// <summary>
        ///     Actualiza el estado de un archivo programado
        /// </summary>
        /// <param name="archivoProgramado">Id de un archivo programado</param>
        /// <param name="estado"></param>
        /// <returns>El archivo programado con el estado enviado</returns>
        private ArchivosProgramados ActualizarEstadosEjecucion(ArchivosProgramados archivoProgramado,
            Models.EEstadosArchivos estado)
        {
            if (archivoProgramado != null)
            {
                archivoProgramado.EstadosArchivos = estado;
            }

            _context.SaveChanges();


            return archivoProgramado;
        }


        /// <summary>
        /// </summary>
        /// <param name="archivoProgramado">Archivo programado a validar</param>
        /// <returns></returns>
        private bool ValidarProgramados(ArchivosProgramados archivoProgramado)
        {
            ContrastarSchema(ConstruirSchema(archivoProgramado), ObtenerArchivoXml(archivoProgramado),archivoProgramado);
            //File.WriteAllLines(Path.Combine(archivoProgramado.UrlArchivoCargado.Replace(".txt", "_error.txt")), _resultadoList.ToArray());

            Task.Factory.StartNew(RegistrarErrores).Wait();
            Task.Factory.StartNew(() => CrearTxtErrores(archivoProgramado)).Wait();


            return true;
        }


        private void CrearTxtErrores(ArchivosProgramados archivoProgramado)
        {
            string urlErrores = string.Empty;
            string nombreArchivo = string.Empty;

            string[] errores = _context.ErroresArchivos
                .Where(c => c.ArchivosProgramadosId == archivoProgramado.ArchivosProgramadosId)
                .Select(c => c.Mensaje)
                .ToArray();


            if (errores.Length > 0)
            {
                nombreArchivo = archivoProgramado.UrlArchivoCargado.Replace(".txt", "_error.txt");
                urlErrores = Path.Combine(nombreArchivo);
                File.WriteAllLines(urlErrores, errores, Encoding.UTF8);
            }


            using (ContextValidadorBd ctx = new ContextValidadorBd())
            {
                ArchivosProgramados auxArchivoProgramado =
                    ctx.ArchivosProgramados.Find(archivoProgramado.ArchivosProgramadosId);
                if (auxArchivoProgramado != null)
                {
                    auxArchivoProgramado.UrlArchivoErrores = urlErrores == string.Empty ? null : nombreArchivo;
                    auxArchivoProgramado.EstadosArchivos = Models.EEstadosArchivos.Terminado;
                    auxArchivoProgramado.EstadosValidacion = urlErrores == string.Empty
                        ? Models.EEstadosValidacion.SinErrores
                        : Models.EEstadosValidacion.ConErrores;
                }

                ctx.SaveChanges();
            }
        }

        /// <summary>
        ///     Metodo que registra los errores generados de contrastar un esquema con un archivo plano
        /// </summary>
        private void RegistrarErrores()
        {
            List<ErroresArchivos> errores = _resultadosList.Select(c => new ErroresArchivos
            {
                Fila = c.Fila,
                Columna = c.Columna,
                ArchivosProgramadosId = c.ArchivosProgramadosId,
                Mensaje = c.Mensaje
            }).ToList();

            if (errores.Count <= 0)
            {
                return;
            }


            if (errores.Count < 5000)
            {
                using (ContextValidadorBd ctx = new ContextValidadorBd())
                {
                    ctx.ErroresArchivos.AddRange(errores);
                    ctx.SaveChanges();
                }
            }
            else
            {
                List<ErroresArchivos> list = new List<ErroresArchivos>();
                foreach (ErroresArchivos error in errores)
                {
                    list.Add(error);

                    if (list.Count <= 5000)
                    {
                        continue;
                    }

                    using (ContextValidadorBd ctx = new ContextValidadorBd())
                    {
                        ctx.ErroresArchivos.AddRange(list);
                        ctx.SaveChanges();
                    }

                    list.Clear();
                }
            }

            //File.WriteAllLines(Path.Combine(archivoProgramado.UrlArchivoCargado.Replace(".txt", "_error.txt")), _resultadosList.ToArray());
        }


        private RestriccionesColumna ObtenerRestricciones(long restriccionesId)
        {
            RestriccionesColumna result;

            using (ContextValidadorBd ctx = new ContextValidadorBd())
            {
                result = ctx.RestriccionesColumnas.Find(restriccionesId);
            }

            return result;
        }


        /// <summary>
        ///     Devuelve el esquema del XSD que va a validar
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns>El Esquema XML contra el que se va a validar</returns>
        private XmlSchema ConstruirSchema(ArchivosProgramados archivo)
        {
            List<Columna> columnas = archivo.Archivo.Columna.OrderBy(c => c.Orden).ToList();

            foreach (Columna columna in columnas)
                if (columna.RestriccionesColumnaId != null)
                {
                    columna.RestriccionesColumna = ObtenerRestricciones((long) columna.RestriccionesColumnaId);
                }


            XmlSchemaElement elementInital = new XmlSchemaElement {Name = "clase"};

            XmlSchemaElement elementItem = new XmlSchemaElement
            {
                Name = "item",
                MinOccurs = Models.MinOccurs,
                MaxOccurs = Models.MaxOccurs,
                SchemaTypeName = new XmlQualifiedName("items", "")
            };

            XmlSchemaComplexType complexTypeColumnas = new XmlSchemaComplexType {Name = "items"};
            XmlSchemaSequence secuenciasColumnas = new XmlSchemaSequence();


            IEnumerable<XmlSchemaElement> xmlSchemaElemen = ColumnasXmlSchemaElement(columnas);
            XmlSchemaComplexType customerType = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();

            XmlSchema shema = new XmlSchema();
            XmlSchemaSet schemaSet = new XmlSchemaSet();


            foreach (XmlSchemaElement xmlSchemaElement in xmlSchemaElemen)
                secuenciasColumnas.Items.Add(xmlSchemaElement);

            complexTypeColumnas.Particle = secuenciasColumnas;

            sequence.Items.Add(elementItem);
            customerType.Particle = sequence;
            elementInital.SchemaType = customerType;


            shema.Items.Add(elementInital);
            shema.Items.Add(complexTypeColumnas);

            foreach (XmlSchemaSimpleType simpleType in ObtenerSimpleTypes(columnas)) shema.Items.Add(simpleType);

            schemaSet.ValidationEventHandler += ValidationCallback;
            schemaSet.Add(shema);
            schemaSet.Compile();

            foreach (XmlSchema schema in schemaSet.Schemas()) shema = schema;

            // Write the complete schema to the Console.

            //shema.Write(Console.Out);
            //Console.ReadLine();

            return shema;
        }


        /// <summary>
        ///     Metodo que constrasta un esquema programado
        /// </summary>
        /// <param name="schemaXml">Esquema contra el que se va a validar</param>
        /// <param name="stringXmlDatos">Archivo programado Xml</param>
        /// <param name="archivoProgramado">Archivo programado </param>
        private void ContrastarSchema(XmlSchema schemaXml, string stringXmlDatos, ArchivosProgramados archivoProgramado)
        {
            if (stringXmlDatos == string.Empty)
            {
                return;
            }

            bool[] validacionErrores = {true};
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            List<Resultados> resultadoList = new List<Resultados>();
            schemaSet.Add(schemaXml);

            ValidadorXml validadorXml = new ValidadorXml(archivoProgramado);

            if (validacionErrores[0])
            {
                resultadoList = validadorXml.ValidarXml(stringXmlDatos, schemaSet);
            }


            foreach (Resultados r in resultadoList) _resultadosList.Add(r);


            //return true;
        }


        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            switch (args.Severity)
            {
                case XmlSeverityType.Warning:
                    Console.Write(@"WARNING: ");
                    break;
                case XmlSeverityType.Error:
                    Console.Write(@"ERROR: ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine(args.Message);
        }

        private IEnumerable<XmlSchemaElement> ColumnasXmlSchemaElement(IEnumerable<Columna> columnas)
        {
            short contador = 0;
            return columnas.Select(columna => new XmlSchemaElement
            {
                Name = "columna" + contador,
                SchemaTypeName = new XmlQualifiedName("s_" + "columna" + contador++, "")
            }).ToList();
        }

        private IEnumerable<XmlSchemaSimpleType> ObtenerSimpleTypes(IEnumerable<Columna> columnas)
        {
            short contador = 0;
            return columnas.Select(columna => new XmlSchemaSimpleType
            {
                Name = "s_" + "columna" + contador++,
                Content = ObtenerSimpleTypeRestriction(columna)
            }).ToList();
        }

        private XmlSchemaSimpleTypeRestriction ObtenerSimpleTypeRestriction(Columna columna)
        {
            XmlSchemaSimpleTypeRestriction respose = new XmlSchemaSimpleTypeRestriction();

            switch (columna.RestriccionesColumna.ComunTipoDato)
            {
                case Models.ETipoDato.Entero:
                    respose.BaseTypeName = new XmlQualifiedName("integer", "http://www.w3.org/2001/XMLSchema");
                    AgregarRestriccionesEntero(respose, columna);
                    break;
                case Models.ETipoDato.Decimal:
                    respose.BaseTypeName = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
                    AgregarRestriccionesDecimal(respose, columna);
                    break;
                case Models.ETipoDato.String:
                    respose.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
                    AgregarRestriccionesString(respose, columna);
                    break;
                case Models.ETipoDato.Date
                    : //Para el tipo de dato date, es importante saber que el solo valida con el formato YYYY-MM-DD
                    respose.BaseTypeName = new XmlQualifiedName("date", "http://www.w3.org/2001/XMLSchema");
                    AgregarRestriccionesDate(respose, columna);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return respose;
        }

        private void AgregarRestriccionesDate(XmlSchemaSimpleTypeRestriction respose, Columna columna)
        {
            if (columna.RestriccionesColumna.DateMaxInclusive != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMaxInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.DateMaxInclusive
                });
            }


            if (columna.RestriccionesColumna.DateMinInclusive != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMinInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.DateMinInclusive
                });
            }

            if (columna.RestriccionesColumna.DatePattern != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaPatternFacet {Value = columna.RestriccionesColumna.DatePattern});
            }

            bool? b = !columna.RestriccionesColumna.DateWhiteSpace;
            if (b != null && (bool) b)
            {
                respose.Facets.Add(new XmlSchemaWhiteSpaceFacet {Value = "collapse"});
            }
        }

        private void AgregarRestriccionesDecimal(XmlSchemaSimpleTypeRestriction respose, Columna columna)
        {
            if (columna.RestriccionesColumna.DecimalTotalDigits != null)
            {
                respose.Facets.Add(new XmlSchemaTotalDigitsFacet
                {
                    Value = columna.RestriccionesColumna.DecimalTotalDigits.ToString()
                });
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.DecimalMaxInclusive)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMaxInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.DecimalMaxInclusive
                });
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.DecimalMinInclusive)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMinInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.DecimalMinInclusive
                });
            }

            if (columna.RestriccionesColumna.DecimalFractionDigits != null)
            {
                respose.Facets.Add(new XmlSchemaFractionDigitsFacet
                {
                    Value = columna.RestriccionesColumna.DecimalFractionDigits.ToString()
                });
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.DecimalPattern))
            {
                respose.Facets.Add(new XmlSchemaPatternFacet {Value = columna.RestriccionesColumna.DecimalPattern});
            }

            bool? b = !columna.RestriccionesColumna.DecimalWhiteSpace;
            if (b != null && (bool) b)
            {
                respose.Facets.Add(new XmlSchemaWhiteSpaceFacet {Value = "collapse"});
            }
        }

        private void AgregarRestriccionesEntero(XmlSchemaSimpleTypeRestriction respose, Columna columna)
        {
            if (columna.RestriccionesColumna.IntMaxInclusive != null)
            {
                respose.Facets.Add(new XmlSchemaMaxInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.IntMaxInclusive.ToString()
                });
            }


            if (columna.RestriccionesColumna.IntMinInclusive != null)
            {
                respose.Facets.Add(new XmlSchemaMinInclusiveFacet
                {
                    Value = columna.RestriccionesColumna.IntMinInclusive.ToString()
                });
            }


            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.IntPattern)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaPatternFacet {Value = columna.RestriccionesColumna.IntPattern});
            }

            bool? b = !columna.RestriccionesColumna.IntWhiteSpace;
            if (b != null && (bool) b)
            {
                respose.Facets.Add(new XmlSchemaWhiteSpaceFacet
                {
                    Value = "collapse"
                }); // Quita todos los espacion en blanco, retornos de carro, etc
            }
        }

        private void AgregarRestriccionesString(XmlSchemaSimpleTypeRestriction respose, Columna columna)
        {
            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.StringLength)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaLengthFacet {Value = columna.RestriccionesColumna.StringLength});
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.StringMaxLength)) //!= string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMaxLengthFacet {Value = columna.RestriccionesColumna.StringMaxLength});
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.StringMinLength)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaMinLengthFacet {Value = columna.RestriccionesColumna.StringMinLength});
            }

            if (!string.IsNullOrEmpty(columna.RestriccionesColumna.StringPattern)) // != string.Empty)
            {
                respose.Facets.Add(new XmlSchemaPatternFacet {Value = columna.RestriccionesColumna.StringPattern});
            }

            bool? b = !columna.RestriccionesColumna.StringWhiteSpace;
            if (b != null && (bool) b)
            {
                respose.Facets.Add(new XmlSchemaWhiteSpaceFacet {Value = "collapse"});
            }
        }
    }
}