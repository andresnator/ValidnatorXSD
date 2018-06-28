using System;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidnatorXSD;
using ValidnatorXSD.BL;
using ValidnatorXSD.Const;
using ValidnatorXSD.DTO;

namespace ValidenatorXSDTest
{
    [TestClass]
    public class ValidnatorXsdTest
    {
        private readonly ConfigDto _paramNotValid;
        private readonly ConfigDto _paramValid;

        public ValidnatorXsdTest()
        {
            _paramValid = new ConfigDto
            {
                SeparatorColumn = EnumsValidnatorXsd.SeparatorColumn.Semicolon,
                PathFile = @"C:\Users\jgonzalg\Source\Repos\ValidnatorXSD\ValidnatorXSDTest\fileTestOk.csv",
                TypeFile = EnumsValidnatorXsd.TypeFile.CsvOrTxt,
                QuantityColumns = 3,
                QuantityRows = 16
            };

            _paramNotValid = new ConfigDto
            {
                SeparatorColumn = EnumsValidnatorXsd.SeparatorColumn.Semicolon,
                PathFile = @"C:\Users\jgonzalg\Source\Repos\ValidnatorXSD\ValidnatorXSDTest\fileTestError.csv",
                TypeFile = EnumsValidnatorXsd.TypeFile.CsvOrTxt,
                QuantityColumns = 3,
                QuantityRows = 15,
                ShemaReader = new XmlTextReader(
                    @"C:\Users\jgonzalg\Source\Repos\ValidnatorXSD\ValidnatorXSDTest\XMLSchemaTest.xsd")
            };
        }

        [TestMethod]
        public void GetXElementFromData()
        {
            //arrange
            var param = _paramValid;
            var instance = new DataFile(_paramValid);

            //act
            var result = instance.GetXElementFromData(new DataFile(_paramValid).GetDataFileModelList());

            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetDataFromFile()
        {
            //arrange
            var param = _paramValid;
            var instance = new DataFile(_paramValid);

            //act
            var result = instance.GetDataFileModelList();

            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetErrorsFromXmlFile()
        {
            //arrange
            var reader =
                new XmlTextReader(
                    @"C:\Users\jgonzalg\Source\Repos\ValidnatorXSD\ValidnatorXSDTest\XMLSchemaTest.xsd");

            _paramValid.ShemaReader = reader;

            var instance = new ValidateFileToXsd(_paramValid);

            //act
            var result =
                instance.GetErrorsFromXmlFile(
                    new DataFile(_paramValid).GetXElementFromData(new DataFile(_paramValid).GetDataFileModelList()));

            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ValidateCountColumns()
        {
            //arrange
            var instance = new DataFile(_paramValid);
            var data = instance.GetDataFileModelList();


            //act
            var instanceTwo = new ValidateFileToXsd(_paramValid);
            var result = instanceTwo.ValidQuantityColumns(data);


            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateCountRows()
        {
            //arrange
            var instance = new DataFile(_paramValid);
            var data = instance.GetDataFileModelList();


            //act
            var instanceTwo = new ValidateFileToXsd(_paramValid);
            var result = instanceTwo.ValidQuantityRows(data);


            //assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void GetRowsErrorQuantity()
        {
            //arrange
            var instance = new DataFile(_paramNotValid);
            var data = instance.GetDataFileModelList();


            //act
            var instanceTwo = new ValidateFileToXsd(_paramNotValid);
            var valid = instanceTwo.ValidQuantityRows(data);

            //assert
            if (valid)
            {
                Assert.IsTrue(true);
                return;
            }

            var result = instanceTwo.GetRowsErrorQuantity(data);
            Assert.IsTrue(result.Message != string.Empty);
        }

        [TestMethod]
        public void GetColumnsErrorQuantity()
        {
            //arrange
            var instance = new DataFile(_paramNotValid);
            var data = instance.GetDataFileModelList();


            //act
            var instanceTwo = new ValidateFileToXsd(_paramNotValid);
            var valid = instanceTwo.ValidQuantityColumns(data);

            //assert
            if (valid)
            {
                Assert.IsTrue(true);
                return;
            }

            var result = instanceTwo.GetColumnsErrorQuantity(data);
            Assert.IsTrue(result.Count > 0);
        }



        [TestMethod]
        public void StartValidate()
        {
            //arrange
            //var reader = new XmlTextReader(@"C:\Users\Administrador\source\repos\ValidnatorXSD\ValidnatorXSDTest\XMLSchemaTest.xsd");

            //_paramNotValid.ShemaReader = reader;
            var instance = new ValidnatorXsd(_paramNotValid);

            //act
            var result = instance.Start();

            //assert
            Debug.WriteLine(new JavaScriptSerializer().Serialize(result));
            Assert.IsNotNull(result);
        }

    }
}