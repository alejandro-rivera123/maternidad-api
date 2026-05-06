using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class ComplicacionMaterna
    {
        [Key]
        public int Id { get; set; }

        public int PartoId { get; set; }
        public string TipoComplicacion { get; set; }
        public string Descripcion { get; set; }
        public string TratamientoAplicado { get; set; }
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("PartoId")]
        public Parto Parto { get; set; }
    }
}