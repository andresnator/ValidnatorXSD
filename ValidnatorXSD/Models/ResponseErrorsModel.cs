namespace ValidnatorXSD.Models
{
    public class ResponseErrorsModel
    {
        public string Message { get; set; }
        public int? ColumnPos { get; set; }
        public int RowPos { get; set; }
    }
}