using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class RecienNacido
    {
        [Key]
        public int Id { get; set; }

        public int PartoId { get; set; }

        public string Nombre { get; set; }

        // CI de la madre - viene de Gestión de Pacientes (Abigail)
        public string CIPaciente { get; set; }

        public DateTime FechaHoraNacimiento { get; set; }
        public decimal PesoGramos { get; set; }
        public decimal TallaCm { get; set; }
        public string Sexo { get; set; }
        public string Estado { get; set; }

        [ForeignKey("PartoId")]
        public Parto Parto { get; set; }

        // Navegación
        public List<SignoVitalNeonatal> SignosVitales { get; set; }
        public List<TratamientoNeonatal> Tratamientos { get; set; }
        public List<AlertaNeonatal> Alertas { get; set; }
        public List<ComplicacionNeonatal> Complicaciones { get; set; }
    }
}