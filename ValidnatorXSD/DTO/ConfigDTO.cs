using System.Xml.Schema;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;

namespace ValidnatorXSD.DTO
{
    public class ConfigDto : IConfig
    {
        public string PathFile { get; set; }
        public XmlSchema ShemaXml { get; set; }
        public EnumsValidnatorXsd.SeparatorsColumn SeparatorColumn { get; set; }
        public EnumsValidnatorXsd.TypeFile TypeFile { get; set; }
        public int CantColumns { get; set; }
        public int? CantRows { get; set; }
    }
}