using System.Xml.Linq;

namespace ValidnatorXSD.IC
{
    internal interface IGetFileXml
    {
        XElement ConvertFileXml(IConfig featureFile);
    }
}