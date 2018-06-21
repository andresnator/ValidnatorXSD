using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Columna", Schema = "VLD")]
    public class Columna
    {
        public Columna()
        {
            Estado = true;
            FechaSistema = DateTime.Now;
        }

        #region Columnas

        public long ColumnaId { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(20, ErrorMessage = "Cantidad Máxima de 20 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(1024, ErrorMessage = "Cantidad Máxima de 1024 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "Campo Requerido")]
        //public Const.Models.ETipoDato TipoDato { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public short Orden { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public short OrdenAnterior { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public DateTime FechaSistema { get; set; }

        #endregion


        #region Relaciones

        public long? RestriccionesColumnaId { get; set; }
        public RestriccionesColumna RestriccionesColumna { get; set; }


        public long ArchivoId { get; set; }
        public Archivo Archivo { get; set; }

        #endregion
    }
}