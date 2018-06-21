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

        public GetFileXmlFabric(EnumsValidatorXsd.TypeFile typeFile)
        {
            switch (typeFile)
            {
                case EnumsValidatorXsd.TypeFile.Xls:
                    break;
                case EnumsValidatorXsd.TypeFile.Xlsx:
                    break;
                case EnumsValidatorXsd.TypeFile.Csv:
                    break;
                case EnumsValidatorXsd.TypeFile.Txt:
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