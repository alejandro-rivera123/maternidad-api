using System.ComponentModel.DataAnnotations;

namespace Maternidad.Dominio
{
    public class TipoSignoVitalNeonatal
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorMax { get; set; }

        // Navegación
        public List<SignoVitalNeonatal> Signos { get; set; }
    }
}