using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maternidad.Dominio
{
    public class TratamientoNeonatal
    {
        [Key]
        public int Id { get; set; }

        public int RecienNacidoId { get; set; }

        // CI del médico que prescribe - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        // CI o código del medicamento - viene de Farmacia (Sergio)
        public string CodigoMedicamento { get; set; }

        public string TipoTratamiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        [ForeignKey("RecienNacidoId")]
        public RecienNacido RecienNacido { get; set; }
    }
}