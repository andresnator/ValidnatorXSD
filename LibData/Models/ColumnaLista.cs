using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibData.Models;

namespace libModelValidador.Modelos
{

    [Table("ColumnaLista", Schema = "VLD")]
    public class ColumnaLista
    {

        public ColumnaLista()
        {
            Estado = true;
            FechaSistema = DateTime.Now;
        }

        #region Columnas

        public long ColumnaListaId { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime FechaSistema { get; set; }


        #endregion


        #region Relaciones

        public long ColumnaId { get; set; }
        public Columna Columna { get; set; }

        public long ListaId { get; set; }
        public Lista Lista { get; set; }

        #endregion

    }
}
