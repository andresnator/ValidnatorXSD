using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("ArchivosProgramados", Schema = "VLD")]
    public class ArchivosProgramados
    {
        public ArchivosProgramados()
        {
            FechaProgramacion = new DateTime();
            Estado = true;
            ErroresArchivos = new List<ErroresArchivos>();
        }


        #region Columnas

        public long ArchivosProgramadosId { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(400, ErrorMessage = "Cantidad Máxima de 300 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Nombre")]
        public string UrlArchivoCargado { get; set; }

        [StringLength(400, ErrorMessage = "Cantidad Máxima de 300 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Nombre")]
        public string UrlArchivoErrores { get; set; }

        public DateTime FechaProgramacion { get; set; }

        public DateTime? FechaValidacion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public Const.Models.EEstadosArchivos EstadosArchivos { get; set; }


        //[Required(ErrorMessage = "Campo Requerido")]
        public Const.Models.EEstadosValidacion? EstadosValidacion { get; set; }

        public bool Estado { get; set; }

        #endregion

        #region Relaciones

        public long ArchivoId { get; set; }
        public Archivo Archivo { get; set; }


        public List<ErroresArchivos> ErroresArchivos { get; set; }

        #endregion
    }
}