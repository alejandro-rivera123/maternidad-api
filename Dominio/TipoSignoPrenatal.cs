using System.ComponentModel.DataAnnotations;

namespace Maternidad.Dominio
{
    public class TipoSignoPrenatal
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorMax { get; set; }

        // Navegación
        public List<ControlPrenatal> Controles { get; set; }
    }
}