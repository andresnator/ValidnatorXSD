using System;
using System.Xml.Linq;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;

namespace ValidnatorXSD.Fabrics
{
    public class GetFileXmlFabric : IGetFileXml
    {
        private readonly IGetFileXml _instance;

        public GetFileXmlFabric()
        {
        }

        public GetFileXmlFabric(EnumsValidnatorXsd.TypeFile typeFile)
        {
            switch (typeFile)
            {
                case EnumsValidnatorXsd.TypeFile.Xls:
                    break;
                case EnumsValidnatorXsd.TypeFile.Xlsx:
                    break;
                case EnumsValidnatorXsd.TypeFile.Csv:
                    break;
                case EnumsValidnatorXsd.TypeFile.Txt:
                    _instance = new GetFileXmlTxt();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeFile), typeFile, null);
            }
        }

        public XElement ConvertFileXml(IConfig feactureFile)
        {
            return _instance.ConvertFileXml(feactureFile);
        }
    }
}