using System.Xml.Schema;
using ValidnatorXSD.Const;

namespace ValidnatorXSD.IC
{
    public interface IConfig
    {
        string PathFile { get; set; }

        XmlSchema ShemaXml { get; set; }

        EnumsValidnatorXsd.SeparatorsColumn SeparatorColumn { get; set; }

        EnumsValidnatorXsd.TypeFile TypeFile { get; set; }

        int CantColumns { get; set; }

        int? CantRows { get; set; }
    }
}