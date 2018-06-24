﻿using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidnatorXSD.BL;
using ValidnatorXSD.Const;
using ValidnatorXSD.DTO;

namespace ValidenatorXSDTest
{
    [TestClass]
    public class ValidnatorXSDTest
    {
        private readonly ConfigDto _param;

        public ValidnatorXSDTest()
        {
            _param = new ConfigDto
            {
                SeparatorColumn = EnumsValidnatorXsd.SeparatorsColumn.Semicolon,
                PathFile = @"C:\Users\Administrador\Documents\fileTest.csv",
                TypeFile = EnumsValidnatorXsd.TypeFile.Csv
            };
        }

        [TestMethod]
        public void GetFileXmlTest()
        {
            //arrange
            var param = _param;
            var instance = new DataFile();

            //act
            var result = instance.GetFileToXElement(param, new DataFile().GetDataFileModelList(param));

            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetDataFromFile()
        {
            //arrange
            var param = _param;
            var instance = new DataFile();

            //act
            var result = instance.GetDataFileModelList(param);

            //assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetXmlFromXsd()
        {
            //arrange
            var reader =
                new XmlTextReader(
                    @"C:\Users\Administrador\source\repos\ValidnatorXSD\ValidnatorXSDTest\XMLSchemaTest.xsd");

            _param.ShemaReader = reader;

            var param = _param;
            var instance = new ValidateFileToXsd();

            //act
            var result = instance.ValidXml(param,
                new DataFile().GetFileToXElement(param, new DataFile().GetDataFileModelList(param)));

            //assert
            Assert.IsNotNull(result);
        }
    }
}