using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table(name: "Parametrizacion", Schema = "Diseno")]
    public class Parametrizacion
    {
        public int ParametrizacionId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(50)]
        [Display(Name = "Codigo:", Description = "Codigo de parametrizacion")]
        public string Llave { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(500, ErrorMessage = "Cantidad Máxima de 500 caracteres")]
        [Display(Name = "Valor:", Description = "Valor de la parametrizacion")]
        public string Valor { get; set; }
    }
}
