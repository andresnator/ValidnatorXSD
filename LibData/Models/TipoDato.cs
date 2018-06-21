using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table(name: "TipoDato", Schema = "Vld")]
    public class TipoDato
    {
        public int TipoDatoId { get; set; }

        public string valor { get; set; }

        public string descripcion { get; set; }
    }
}
