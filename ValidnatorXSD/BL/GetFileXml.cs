using System.Linq;
using System.Xml.Linq;
using ValidnatorXSD.Const;
using ValidnatorXSD.IC;

namespace ValidnatorXSD.BL
{
    public class GetFileXml : IGetFileXml
    {
        public XElement ConvertFileXml(IConfig fileFeature)
        {
            var dataFile = new GetDataFile().GetDataFileModel(fileFeature);

            var resultXml = new XElement(ComunConst.Table,
                from item in dataFile
                select new XElement(ComunConst.Row, from ir in item.ItemsRow
                    select new XElement(ComunConst.Column + ir.CounterCol, ir.ValueCol)));

            return resultXml;
        }
    }
}