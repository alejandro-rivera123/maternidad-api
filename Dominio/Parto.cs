using System.ComponentModel.DataAnnotations;

namespace Maternidad.Dominio
{
    public class Parto
    {
        [Key]
        public int Id { get; set; }

        // CI del paciente - viene de Gestión de Pacientes (Abigail)
        public string CIPaciente { get; set; }

        // CI del médico - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        public DateTime FechaHora { get; set; }
        public string TipoParto { get; set; }
        public string Anestesia { get; set; }
        public string Complicaciones { get; set; }
        public string Observaciones { get; set; }

        // Navegación
        public RecienNacido RecienNacido { get; set; }
        public ComplicacionMaterna ComplicacionMaterna { get; set; }
        public List<SeguimientoPrenatal> SeguimientosPrenatal { get; set; }
    }
}