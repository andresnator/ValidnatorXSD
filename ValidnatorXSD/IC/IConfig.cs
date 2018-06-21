using System.Xml.Schema;
using ValidenatorXSD.Const;

namespace ValidenatorXSD.DTO
{
    public interface IConfig
    {
        string pathFile { get; set; }

        XmlSchema shemaXml { get; set; }

        EnumsValidatorXSD.SeparatorsColumn separatorColumn { get; set; }

        EnumsValidatorXSD.TypeFile typeFile { get; set; }

        int cantColumns { get; set; }

        int? cantRows { get; set; }
    }
}