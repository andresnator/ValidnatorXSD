using System.Xml.Schema;
using ValidenatorXSD.Const;

namespace ValidenatorXSD.DTO
{
    public interface IConfig
    {
        string pathFile { get; set; }

        XmlSchema shemaXml { get; set; }

        EnumsValidatorXsd.SeparatorsColumn separatorColumn { get; set; }

        EnumsValidatorXsd.TypeFile typeFile { get; set; }

        int cantColumns { get; set; }

        int? cantRows { get; set; }
    }
}