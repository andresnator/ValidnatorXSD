using System.Xml.Schema;
using ValidenatorXSD.Const;

namespace ValidenatorXSD.DTO
{
    public class ConfigDTO : IConfig
    {
        public string pathFile { get; set; }
        public XmlSchema shemaXml { get; set; }
        public EnumsValidatorXSD.SeparatorsColumn separatorColumn { get; set; }
        public EnumsValidatorXSD.TypeFile typeFile { get; set; }
        public int cantColumns { get; set; }
        public int? cantRows { get; set; }
    }
}