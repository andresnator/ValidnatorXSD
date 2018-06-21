using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Extension", Schema = "Vld")]
    public class Extension
    {
        public Extension(){
            estado = true;
        }

        public int ExtensionId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(5, ErrorMessage = "Cantidad Máxima de 5 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Codigo extension", Description = "Codigo de la extension")]
        public string cod_extension { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(50, ErrorMessage = "Cantidad Máxima de 50 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Descripcion Extension", Description = "Descripcion de la extension")]
        public string desc_extension { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public bool estado { get; set; }
    }
}
