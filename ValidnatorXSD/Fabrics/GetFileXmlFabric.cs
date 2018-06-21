using System;
using System.Xml.Linq;
using ValidenatorXSD.Const;
using ValidenatorXSD.IC;

namespace ValidenatorXSD.Fabrics
{
    public class GetFileXmlFabric : IGetFileXml
    {
        private readonly IGetFileXml instance;

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
                    instance = new GetFileXmlTxt();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeFile), typeFile, null);
            }
        }

        public XElement ConvertFileXml(string urlFile)
        {
            return instance.ConvertFileXml(urlFile);
        }
    }
}