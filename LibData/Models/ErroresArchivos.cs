using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("ErroresArchivos", Schema = "VLD")]
    public class ErroresArchivos
    {
        public ErroresArchivos()
        {
            Estado = true;
            FechaRegistro = DateTime.Now;
            ErroresArchivosId = Guid.NewGuid();
        }

        [Key] public Guid ErroresArchivosId { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(2000, ErrorMessage = "Cantidad Máxima de 2000 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Mensaje")]
        public string Mensaje { get; set; }

        [Required] public long Columna { get; set; }

        [Required] public long Fila { get; set; }

        [Required] public bool Estado { get; set; }

        [Required] public DateTime FechaRegistro { get; set; }


        [Required] public long ArchivosProgramadosId { get; set; }

        public ArchivosProgramados ArchivosProgramados { get; set; }
    }
}