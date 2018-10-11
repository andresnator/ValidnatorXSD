// Proyecto: ValidnatorXSD
// Nombre: IDataFileDTO.cs
// Descripción:  
// Fecha Creación: 2018/10/10  1:46 p. m.   <AAAA/MM/DD>  
// Compañia: UTES
// Desarrollador: Jose Andres González Guevara

using System.Collections.Generic;
using ValidnatorXSD.Models;

namespace ValidnatorXSD.IC
{
    public interface IDataFileDTO
    {
        int RowNumber { get; set; }
        int QuantityColumns { get; set; }
        string DataRow { get; set; }
        int InitCounterCol { get; set; }

        List<RowDataFileModel> ItemsRow { get; set; }
    }
}