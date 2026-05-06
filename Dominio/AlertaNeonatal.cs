using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class AlertaNeonatal
    {
        [Key]
        public int Id { get; set; }

        public int RecienNacidoId { get; set; }
        public int TipoSignoId { get; set; }
        public string TipoAlerta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaGenerada { get; set; }

        [ForeignKey("RecienNacidoId")]
        public RecienNacido RecienNacido { get; set; }

        [ForeignKey("TipoSignoId")]
        public TipoSignoVitalNeonatal TipoSigno { get; set; }
    }
}