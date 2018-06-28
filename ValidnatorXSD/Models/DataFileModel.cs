using System.Collections.Generic;

namespace ValidnatorXSD.Models
{
    public class DataFileModel
    {
        public int RowNumber { get; set; }
        public int QuantityColumns { get; set; }
        public string DataRow { get; set; }
        public int InitCounterCol { get; set; }

        public List<RowDataFileModel> ItemsRow { get; set; }
    }
}