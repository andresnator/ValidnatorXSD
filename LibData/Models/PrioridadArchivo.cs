using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("PrioridadArchivo", Schema = "Vld")]
    public class PrioridadArchivo
    {
        public int PrioridadArchivoId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(2, ErrorMessage = "Cantidad Máxima de 2 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Valor prioridad", Description = "Valor de la propiedad")]
        public string valor { get; set; }

        [StringLength(50, ErrorMessage = "Cantidad Máxima de 50 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Prioridad", Description = "Descripcion de la prioridad")]
        public string descripcion { get; set; }

    }
}
