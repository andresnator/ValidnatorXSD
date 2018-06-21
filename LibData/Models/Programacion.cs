using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using libModelValidador.Modelos;

namespace LibData.Models
{
    [Table("Programacion", Schema = "Vld")]
    public class Programacion
    {
        public Programacion()
        {
            fecha = DateTime.Now;
        }

        public int ProgramacionId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(250)]
        [Display(Name = "Ruta Archivo:", Description = "Ruta del archivo")]
        public string ruta { get; set; }

        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int ArchivoId { get; set; }
        public virtual ArchivoNorma ArchivoNorma { get; set; }

    }
}
