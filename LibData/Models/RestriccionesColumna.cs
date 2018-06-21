using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{

    [Table("RestriccionesColumna", Schema = "VLD")]
    public class RestriccionesColumna
    {
        public RestriccionesColumna()
        {
            ComunEstado = true;
            ComunFechaSitema = DateTime.Now;
            Columnas = new List<Columna>();
        }

        #region Columnas

        public long RestriccionesColumnaId { get; set; }

        #region Comunes (Aplica para todo tipo de dato)

        /// <summary>
        /// Nombre de la resctricción 
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(20, ErrorMessage = "Cantidad Máxima de 20 caracteres")]
        [Column(TypeName = "varchar")]
        public string ComunNombre { get; set; }

        /// <summary>
        /// Descripcion donde se guardara el tipo de resctricción con el objetivo que esta parametrización pueda volver a ser usada
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(256, ErrorMessage = "Cantidad Máxima de 256 caracteres")]
        [Column(TypeName = "varchar")]
        public string ComunDescripcion { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public bool ComunEstado { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public DateTime ComunFechaSitema { get; set; }


        //[Required(ErrorMessage = "Campo requerido")]
        //public Const.Models.ETipoDato TipoDato { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public Const.Models.ETipoDato ComunTipoDato { get; set; }

        #endregion

        #region Enteros

        public int? IntMinInclusive { get; set; }
        public int? IntMaxInclusive { get; set; }
        public string IntPattern { get; set; }
        public bool? IntWhiteSpace { get; set; }

        #endregion

        #region String

        public string StringLength { get; set; }
        public string StringMaxLength { get; set; }
        public string StringMinLength { get; set; }
        public string StringPattern { get; set; }
        public bool? StringWhiteSpace { get; set; }

        #endregion

        #region Decimal

        public int? DecimalFractionDigits { get; set; }
        public string DecimalMaxInclusive { get; set; }
        public string DecimalMinInclusive { get; set; }
        public string DecimalPattern { get; set; }
        public int? DecimalTotalDigits { get; set; }
        public bool? DecimalWhiteSpace { get; set; }

        #endregion

        #region Date

        public string DateMaxInclusive { get; set; }
        public string DateMinInclusive { get; set; }
        public string DatePattern { get; set; }
        public bool? DateWhiteSpace { get; set; }


        #endregion

        #endregion

        #region Relaciones
        public List<Columna> Columnas { get; set; }
        #endregion

    }
}
