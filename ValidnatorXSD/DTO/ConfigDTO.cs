using System.Xml.Schema;
using ValidenatorXSD.Const;

namespace ValidenatorXSD.DTO
{
    public class ConfigDto : IConfig
    {
        public string pathFile { get; set; }
        public XmlSchema shemaXml { get; set; }
        public EnumsValidatorXsd.SeparatorsColumn separatorColumn { get; set; }
        public EnumsValidatorXsd.TypeFile typeFile { get; set; }
        public int cantColumns { get; set; }
        public int? cantRows { get; set; }
    }
}