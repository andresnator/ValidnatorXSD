using System.Xml;
using ValidnatorXSD.Const;

namespace ValidnatorXSD.IC
{
    public interface IConfig
    {
        string PathFile { get; set; }

        XmlTextReader ShemaReader { get; set; }

        EnumsValidnatorXsd.SeparatorsColumn SeparatorColumn { get; set; }

        EnumsValidnatorXsd.TypeFile TypeFile { get; set; }

        int QuantityColumns { get; set; }

        int? QuantityRows { get; set; }
    }
}