using System.Xml;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;

namespace ValidnatorXSD.DTO
{
    public class ConfigDto : IConfig
    {
        public string PathFile { get; set; }
        public XmlTextReader ShemaReader { get; set; }
        public EnumsValidnatorXsd.SeparatorsColumn SeparatorColumn { get; set; }
        public EnumsValidnatorXsd.TypeFile TypeFile { get; set; }
        public int QuantityColumns { get; set; }
        public int? QuantityRows { get; set; }
    }
}