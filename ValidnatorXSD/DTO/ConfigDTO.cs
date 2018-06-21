using System.Xml.Schema;
using ValidenatorXSD.Const;
using ValidenatorXSD.IC;

namespace ValidenatorXSD.DTO
{
    public class ConfigDto : IConfig
    {
        public string pathFile { get; set; }
        public XmlSchema shemaXml { get; set; }
        public EnumsValidnatorXsd.SeparatorsColumn separatorColumn { get; set; }
        public EnumsValidnatorXsd.TypeFile typeFile { get; set; }
        public int cantColumns { get; set; }
        public int? cantRows { get; set; }
    }
}