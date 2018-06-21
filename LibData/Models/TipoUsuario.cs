using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table(name: "TipoUsuario", Schema = "Basicas")]
    public class TipoUsuario
    {
        public int TipoUsuarioId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(10, ErrorMessage = "Cantidad Máxima de 10 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Codigo:", Description = "Codigo de tipo usuario")]
        public string cod_tipousuario { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(50, ErrorMessage = "Cantidad Máxima de 50 caracteres")]
        [Display(Name = "Descripcion:", Description = "Descripcion de tipo usuario")]
        public string desc_tipousuario { get; set; }
    }
}
