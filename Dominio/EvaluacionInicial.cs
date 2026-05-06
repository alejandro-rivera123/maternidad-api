using System.ComponentModel.DataAnnotations;


namespace Maternidad.Dominio
{
    public class EvaluacionInicial
    {
        [Key]
        public int Id { get; set; }

        public int RecienNacidoId { get; set; }

        // CI del médico/enfermera - viene de Recursos Humanos (Rodrigo)
        public string CIEmpleado { get; set; }

        public int ApgarMin1 { get; set; }
        public int ApgarMin5 { get; set; }
        public DateTime FechaEvaluacion { get; set; }

        public RecienNacido RecienNacido { get; set; }
    }
}