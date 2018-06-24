using System.Collections.Generic;

namespace ValidnatorXSD.Models
{
    public class DataFileModel
    {
        public long RowNumber { get; set; }
        public int CantColumns { get; set; }
        public string DataRow { get; set; }
        public long InitCounterCol { get; set; }

        public List<RowDataFileModel> ItemsRow { get; set; }
    }
}