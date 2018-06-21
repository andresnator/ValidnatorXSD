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

        public GetFileXmlFabric(EnumsValidatorXSD.TypeFile typeFile)
        {
            switch (typeFile)
            {
                case EnumsValidatorXSD.TypeFile.Xls:
                    break;
                case EnumsValidatorXSD.TypeFile.Xlsx:
                    break;
                case EnumsValidatorXSD.TypeFile.Csv:
                    break;
                case EnumsValidatorXSD.TypeFile.Txt:
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