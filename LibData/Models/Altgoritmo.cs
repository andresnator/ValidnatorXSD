using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Altgoritmo", Schema = "VLD")]
    public class Altgoritmo
    {
        public Altgoritmo()
        {
            Estado = true;
            FechaSistema = DateTime.Now;

        }

        #region Columnas


        public long AltgoritmoId { get; set; }


        [Required(ErrorMessage = "Campo Requerido"), StringLength(20, ErrorMessage = "Cantidad Máxima de 20 caracteres")]
        [Column(TypeName = "varchar")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Campo Requerido"), StringLength(20, ErrorMessage = "Cantidad Máxima de 20 caracteres")]
        [Column(TypeName = "varchar")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Campo Requerido"), StringLength(256, ErrorMessage = "Cantidad Máxima de 256 caracteres")]
        [Column(TypeName = "varchar")]
        public string Expresion { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaSistema { get; set; }

        #endregion

        #region Relaciones

        //public long ColumnaId { get; set; }
        //public Columna Columna { get; set; }


        #endregion
    }
}
