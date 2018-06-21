using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using libModelValidador.Modelos;

namespace LibData.Models
{
    [Table("EstructuraArchivo", Schema = "Vld")]
    public class EstructuraArchivo
    {
        public int EstructuraArchivoId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Nombre Columna", Description = "Nombre de la columna")]
        public string nombreColumna { get; set; }

        public int ordenColumna { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int TipoDatoId { get; set; }
        public virtual TipoDato TipoDato { get; set; }

        public int longitud { get; set; }

        public string delimitador { get; set; }
    }
}
