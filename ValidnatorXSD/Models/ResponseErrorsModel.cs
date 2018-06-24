namespace ValidnatorXSD.Models
{
    public class ResponseErrorsModel
    {
        public string Message { get; set; }
        public long? ColumnPos { get; set; }
        public long RowPos { get; set; }
    }
}