using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class ControlPrenatal
    {
        [Key]
        public int Id { get; set; }

        public int SeguimientoId { get; set; }

        // ID de la cita - viene de Gestión de Turnos y Citas (Franz/Jonathan)
        public int CitaId { get; set; }

        public DateTime FechaControl { get; set; }
        public int SemanaGestacion { get; set; }
        public int TipoSignoPrenatalId { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("SeguimientoId")]
        public SeguimientoPrenatal Seguimiento { get; set; }

        [ForeignKey("TipoSignoPrenatalId")]
        public TipoSignoPrenatal TipoSigno { get; set; }
    }
}