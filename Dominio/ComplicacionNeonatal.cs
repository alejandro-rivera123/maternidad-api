using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class ComplicacionNeonatal
    {
        [Key]
        public int Id { get; set; }

        public int RecienNacidoId { get; set; }

        // CI de la enfermera que registra - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        public string TipoComplicacion { get; set; }
        public string Descripcion { get; set; }
        public string TratamientoAplicado { get; set; }
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("RecienNacidoId")]
        public RecienNacido RecienNacido { get; set; }
    }
}