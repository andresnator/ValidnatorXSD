// Proyecto: ValidnatorXSD
// Nombre: IResponseErrorsDTO.cs
// Descripción:  
// Fecha Creación: 2018/10/11  4:53 a. m.   <AAAA/MM/DD>  
// Compañia: UTES
// Desarrollador: 

namespace ValidnatorXSD.IC
{
    public interface IResponseErrorsDTO
    {
        string Message { get; set; }
        int? ColumnPos { get; set; }
        int RowPos { get; set; }
    }
}