using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibData.Models
{
    [Table("Archivo", Schema = "VLD")]
    public class Archivo
    {
        public Archivo()
        {
            Estado = true;
            FechaSistema = DateTime.Now;
            Columna = new List<Columna>();
            ArchivosProgramados = new List<ArchivosProgramados>();
        }

        #region Columnas Tabla

        public long ArchivoId { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(50, ErrorMessage = "Cantidad Máxima de 50 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Nombre archivo", Description = "Nombre del archivo")]
        public string Nombre { get; set; }

        [StringLength(1024, ErrorMessage = "Cantidad Máxima de 1024 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Descripcion", Description = "Descripcion del archivo")]
        public string Descripcion { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Cantidad de columnas", Description = "Numero de columnas")]
        public int CantidadColumnas { get; set; }
        
        [Required(ErrorMessage = "Campo Requerido"), StringLength(5, ErrorMessage = "Cantidad Máxima de 5 caracteres")]
        public string Separador { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public DateTime FechaSistema { get; set; }

        //[NotMapped]
        public int CantidadColumnasHabilitadas { get; set; }

        //[NotMapped]
        public int CantidadColumnasInHabilitadas { get; set; }

        #endregion

        #region Relaciones

        public List<Columna> Columna { get; set; }

        public List<ArchivosProgramados> ArchivosProgramados { get; set; }

        #endregion

    }
}
