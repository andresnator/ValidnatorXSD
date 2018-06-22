using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidnatorXSD.BL;
using ValidnatorXSD.Const;
using ValidnatorXSD.DTO;

namespace ValidenatorXSDTest
{
    [TestClass]
    public class ValidnatorXSDTest
    {
        [TestMethod]
        public void GetFileXmlTest()
        {
            //arrange
            var param = new ConfigDto
            {
                SeparatorColumn = EnumsValidnatorXsd.SeparatorsColumn.Semicolon,
                PathFile = @"C:\Users\Administrador\Documents\fileTest.csv",
                TypeFile = EnumsValidnatorXsd.TypeFile.Txt
            };

            var instance = new GetFileXml();

            //act
            var result = instance.ConvertFileXml(param);

            //assert
            Assert.IsNotNull(result);
        }
    }
}