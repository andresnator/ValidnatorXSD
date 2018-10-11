using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidnatorXSD;
using ValidnatorXSD.Const;
using ValidnatorXSD.DTO;
using ValidnatorXSD.Models;

namespace ValidenatorXSDTest
{
    [TestClass]
    public class ValidnatorXsdTest
    {
        private readonly ConfigDto paramNotValid;

        public ValidnatorXsdTest()
        {
            paramNotValid = new ConfigDto
            {
                SeparatorColumn = EnumsValidnatorXsd.SeparatorColumn.Semicolon,
                PathFile = $"{path}fileTestError.csv",
                TypeFile = EnumsValidnatorXsd.TypeFile.Txt,
                QuantityColumns = 7,
                //QuantityRows = 15,
                ShemaReader = new XmlTextReader($"{path}XMLSchemaTest.xsd")
            };
        }

        private string path { get; } =
            @"C:\Users\jgonzalg\Desktop\ValidnatorXSD-master (1)\ValidnatorXSD-master\ValidnatorXSDTest\";


        [TestMethod]
        public void StartValidate()
        {
            ValidnatorXsd instance = new ValidnatorXsd(paramNotValid);

            List<ResponseErrorsModel> result = instance.Start();

            Assert.IsNotNull(result);
        }
    }
}