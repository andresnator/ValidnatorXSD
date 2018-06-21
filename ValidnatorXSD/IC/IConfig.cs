using System.Xml.Schema;
using ValidenatorXSD.Const;

namespace ValidenatorXSD.IC
{
    public interface IConfig
    {
        string pathFile { get; set; }

        XmlSchema shemaXml { get; set; }

        EnumsValidnatorXsd.SeparatorsColumn separatorColumn { get; set; }

        EnumsValidnatorXsd.TypeFile typeFile { get; set; }

        int cantColumns { get; set; }

        int? cantRows { get; set; }
    }
}