using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Estado", Schema = "Vld")]
    public class Estado
    {
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(10, ErrorMessage = "Cantidad Máxima de 10 caracteres")]
        [Display(Name = "Codigo:", Description = "Codigo del estado")]
        public string cod_estado { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(150, ErrorMessage = "Cantidad Máxima de 150 caracteres")]
        [Display(Name = "Descripcion:", Description = "Descripcion del estado")]
        public string desc_estado { get; set; }
    }
}
