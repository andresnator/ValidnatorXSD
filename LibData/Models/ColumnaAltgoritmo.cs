using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("ColumnaAltgoritmo", Schema = "VLD")]
    public class ColumnaAltgoritmo
    {
        public ColumnaAltgoritmo()
        {
            Estado = true;
            FechaSistema = DateTime.Now;
        }

        #region Columnas

        public long ColumnaAltgoritmoId { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public DateTime FechaSistema { get; set; }
        

        #endregion

        #region Relaciones

        public long ColumnaId { get; set; }
        public Columna Columna { get; set; }

        public long AltgoritmoId { get; set; }
        public Altgoritmo Altgoritmo { get; set; }
        

        #endregion

        
    }
}
