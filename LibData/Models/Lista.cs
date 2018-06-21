using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Lista", Schema = "VLD")]
    public class Lista
    {
        public Lista()
        {
            Estado = true;
            FechaSistema = DateTime.Now;
        }


        #region Columnas


        public long ListaId { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(20, ErrorMessage = "Cantidad Máxima de 20 caracteres"), Column(TypeName = "varchar")]
        public string Valor { get; set; }


        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaSistema { get; set; }


        #endregion


    }
}
