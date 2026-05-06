using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class SignoVitalNeonatal
    {
        [Key]
        public int Id { get; set; }

        public int RecienNacidoId { get; set; }
        public int TipoSignoId { get; set; }

        // CI de la enfermera que toma el signo - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        public DateTime FechaHora { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("RecienNacidoId")]
        public RecienNacido RecienNacido { get; set; }

        [ForeignKey("TipoSignoId")]
        public TipoSignoVitalNeonatal TipoSigno { get; set; }
    }
}