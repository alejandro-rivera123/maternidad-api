using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class SeguimientoPrenatal
    {
        [Key]
        public int Id { get; set; }

        // CI del paciente - viene de Gestión de Pacientes (Abigail)
        public string CIPaciente { get; set; }

        // CI del médico - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }

        // FK hacia Parto (nullable porque el seguimiento empieza antes del parto)
        public int? PartoId { get; set; }

        [ForeignKey("PartoId")]
        public Parto Parto { get; set; }

        // Navegación
        public List<ControlPrenatal> Controles { get; set; }
    }
}