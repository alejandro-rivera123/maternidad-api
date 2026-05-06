using System.ComponentModel.DataAnnotations;

/////////////////////////////////////esta clase olo esta para pruebas/////////////////////////////////////////////////////////////
namespace Maternidad.Dominio
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CI { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }

        // Relaciones
        public List<SeguimientoPrenatal> Seguimientos { get; set; }
        public List<Parto> Partos { get; set; }
    }
}