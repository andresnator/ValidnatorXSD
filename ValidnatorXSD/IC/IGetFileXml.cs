using System.Xml.Linq;

namespace ValidenatorXSD.IC
{
    internal interface IGetFileXml
    {
        XElement ConvertFileXml(string urlFile);
    }
}