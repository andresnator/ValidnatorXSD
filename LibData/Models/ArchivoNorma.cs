using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using libModelValidador.Modelos;

namespace LibData.Models
{
    [Table("ArchivoNorma", Schema = "Vld")]
    public class ArchivoNorma
    {
        public ArchivoNorma()
        {
            estado = true;
        }

        public int ArchivoNormaId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(50, ErrorMessage = "Cantidad Máxima de 50 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Titulo norma", Description = "Titulo de la norma")]
        public string titulo { get; set; }


        [StringLength(1024, ErrorMessage = "Cantidad Máxima de 1024 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Descripcion", Description = "Descripcion del archivo")]
        public string descripcion { get; set; }


        [StringLength(1, ErrorMessage = "Cantidad Máxima de 1 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Delimitador", Description = "Delimitador del archivo")]
        public string delimitador { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int ExtensionId { get; set; }
        public virtual Extension Extension { get; set; }
        
        [Required(ErrorMessage = "Campo requerido")]
        public bool estado { get; set; }
    }
}
