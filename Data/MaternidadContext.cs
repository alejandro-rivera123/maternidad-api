using Microsoft.EntityFrameworkCore;
using Maternidad.Dominio;

namespace Maternidad.Data
{
    public class MaternidadContext : DbContext
    {
        public MaternidadContext(DbContextOptions<MaternidadContext> options)
            : base(options)
        {
        }

        public DbSet<SeguimientoPrenatal> SeguimientoPrenatal { get; set; }
        public DbSet<ControlPrenatal> ControlPrenatal { get; set; }
        public DbSet<TipoSignoPrenatal> TipoSignoPrenatal { get; set; }
        public DbSet<TipoSignoVitalNeonatal> TipoSignoVitalNeonatal { get; set; }
        public DbSet<Parto> Parto { get; set; }
        public DbSet<RecienNacido> RecienNacido { get; set; }
        public DbSet<SignoVitalNeonatal> SignoVitalNeonatal { get; set; }
        public DbSet<EvaluacionInicial> EvaluacionInicial { get; set; }
        public DbSet<TratamientoNeonatal> TratamientoNeonatal { get; set; }
        public DbSet<AlertaNeonatal> AlertaNeonatal { get; set; }
        public DbSet<ComplicacionNeonatal> ComplicacionNeonatal { get; set; }
        public DbSet<ComplicacionMaterna> ComplicacionMaterna { get; set; }

        // Helper para no repetir tanto
        private static DateTime Utc(int y, int m, int d, int h = 0, int min = 0, int s = 0)
            => DateTime.SpecifyKind(new DateTime(y, m, d, h, min, s), DateTimeKind.Utc);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. TipoSignoPrenatal
            modelBuilder.Entity<TipoSignoPrenatal>().HasData(
                new TipoSignoPrenatal { Id = 1, Nombre = "Presión Arterial Sistólica", Unidad = "mmHg", ValorMin = 90, ValorMax = 140 },
                new TipoSignoPrenatal { Id = 2, Nombre = "Frecuencia Cardíaca", Unidad = "bpm", ValorMin = 60, ValorMax = 100 },
                new TipoSignoPrenatal { Id = 3, Nombre = "Peso", Unidad = "kg", ValorMin = 45, ValorMax = 120 },
                new TipoSignoPrenatal { Id = 4, Nombre = "Altura Uterina", Unidad = "cm", ValorMin = 20, ValorMax = 38 },
                new TipoSignoPrenatal { Id = 5, Nombre = "Glucosa", Unidad = "mg/dL", ValorMin = 70, ValorMax = 110 }
            );

            // 2. TipoSignoVitalNeonatal
            modelBuilder.Entity<TipoSignoVitalNeonatal>().HasData(
                new TipoSignoVitalNeonatal { Id = 1, Nombre = "Frecuencia Cardíaca", Unidad = "bpm", ValorMin = 100, ValorMax = 160 },
                new TipoSignoVitalNeonatal { Id = 2, Nombre = "Temperatura", Unidad = "°C", ValorMin = 36.5m, ValorMax = 37.5m },
                new TipoSignoVitalNeonatal { Id = 3, Nombre = "Saturación de Oxígeno", Unidad = "%", ValorMin = 95, ValorMax = 100 },
                new TipoSignoVitalNeonatal { Id = 4, Nombre = "Frecuencia Respiratoria", Unidad = "rpm", ValorMin = 30, ValorMax = 60 },
                new TipoSignoVitalNeonatal { Id = 5, Nombre = "Glucosa Neonatal", Unidad = "mg/dL", ValorMin = 45, ValorMax = 125 }
            );

            // 3. Parto
            modelBuilder.Entity<Parto>().HasData(
                new Parto { Id = 1, CIPaciente = "7654321", CIEmpleado = "1234567", FechaHora = Utc(2024, 1, 15, 8, 30), TipoParto = "Natural", Anestesia = "Epidural", Complicaciones = "Ninguna", Observaciones = "Parto sin complicaciones" },
                new Parto { Id = 2, CIPaciente = "8765432", CIEmpleado = "2345678", FechaHora = Utc(2024, 2, 20, 14, 0), TipoParto = "Cesárea", Anestesia = "General", Complicaciones = "Sangrado leve", Observaciones = "Se requirió cesárea de emergencia" },
                new Parto { Id = 3, CIPaciente = "9876543", CIEmpleado = "1234567", FechaHora = Utc(2024, 3, 10, 22, 15), TipoParto = "Natural", Anestesia = "Ninguna", Complicaciones = "Ninguna", Observaciones = "Parto rápido sin intervención" },
                new Parto { Id = 4, CIPaciente = "6543210", CIEmpleado = "3456789", FechaHora = Utc(2024, 4, 5, 11, 45), TipoParto = "Cesárea", Anestesia = "Epidural", Complicaciones = "Ninguna", Observaciones = "Cesárea programada" },
                new Parto { Id = 5, CIPaciente = "5432109", CIEmpleado = "2345678", FechaHora = Utc(2024, 5, 18, 7, 0), TipoParto = "Natural", Anestesia = "Epidural", Complicaciones = "Desgarro leve", Observaciones = "Se realizó episiotomía" }
            );

            // 4. RecienNacido
            modelBuilder.Entity<RecienNacido>().HasData(
                new RecienNacido { Id = 1, PartoId = 1, Nombre = "Bebé García", CIPaciente = "7654321", FechaHoraNacimiento = Utc(2024, 1, 15, 8, 45), PesoGramos = 3200, TallaCm = 50, Sexo = "Masculino", Estado = "Sano" },
                new RecienNacido { Id = 2, PartoId = 2, Nombre = "Bebé Mamani", CIPaciente = "8765432", FechaHoraNacimiento = Utc(2024, 2, 20, 14, 30), PesoGramos = 2900, TallaCm = 48, Sexo = "Femenino", Estado = "En observación" },
                new RecienNacido { Id = 3, PartoId = 3, Nombre = "Bebé Flores", CIPaciente = "9876543", FechaHoraNacimiento = Utc(2024, 3, 10, 22, 20), PesoGramos = 3500, TallaCm = 52, Sexo = "Masculino", Estado = "Sano" },
                new RecienNacido { Id = 4, PartoId = 4, Nombre = "Bebé Quispe", CIPaciente = "6543210", FechaHoraNacimiento = Utc(2024, 4, 5, 12, 0), PesoGramos = 2750, TallaCm = 47, Sexo = "Femenino", Estado = "En observación" },
                new RecienNacido { Id = 5, PartoId = 5, Nombre = "Bebé Chávez", CIPaciente = "5432109", FechaHoraNacimiento = Utc(2024, 5, 18, 7, 10), PesoGramos = 3800, TallaCm = 54, Sexo = "Masculino", Estado = "Sano" }
            );

            // 5. SeguimientoPrenatal
            modelBuilder.Entity<SeguimientoPrenatal>().HasData(
                new SeguimientoPrenatal { Id = 1, CIPaciente = "7654321", CIEmpleado = "1234567", FechaInicio = Utc(2023, 5, 1), Estado = "Completado", PartoId = 1 },
                new SeguimientoPrenatal { Id = 2, CIPaciente = "8765432", CIEmpleado = "2345678", FechaInicio = Utc(2023, 7, 15), Estado = "Completado", PartoId = 2 },
                new SeguimientoPrenatal { Id = 3, CIPaciente = "9876543", CIEmpleado = "1234567", FechaInicio = Utc(2023, 8, 20), Estado = "Completado", PartoId = 3 },
                new SeguimientoPrenatal { Id = 4, CIPaciente = "6543210", CIEmpleado = "3456789", FechaInicio = Utc(2023, 9, 10), Estado = "Completado", PartoId = 4 },
                new SeguimientoPrenatal { Id = 5, CIPaciente = "5432109", CIEmpleado = "2345678", FechaInicio = Utc(2023, 11, 1), Estado = "Completado", PartoId = 5 }
            );

            // 6. ControlPrenatal
            modelBuilder.Entity<ControlPrenatal>().HasData(
                new ControlPrenatal { Id = 1, SeguimientoId = 1, CitaId = 101, FechaControl = Utc(2023, 7, 1), SemanaGestacion = 12, TipoSignoPrenatalId = 1, Valor = 110 },
                new ControlPrenatal { Id = 2, SeguimientoId = 1, CitaId = 102, FechaControl = Utc(2023, 9, 15), SemanaGestacion = 24, TipoSignoPrenatalId = 2, Valor = 80 },
                new ControlPrenatal { Id = 3, SeguimientoId = 1, CitaId = 103, FechaControl = Utc(2023, 11, 20), SemanaGestacion = 36, TipoSignoPrenatalId = 3, Valor = 68 },
                new ControlPrenatal { Id = 4, SeguimientoId = 2, CitaId = 104, FechaControl = Utc(2023, 9, 1), SemanaGestacion = 10, TipoSignoPrenatalId = 1, Valor = 115 },
                new ControlPrenatal { Id = 5, SeguimientoId = 2, CitaId = 105, FechaControl = Utc(2023, 11, 10), SemanaGestacion = 22, TipoSignoPrenatalId = 4, Valor = 28 },
                new ControlPrenatal { Id = 6, SeguimientoId = 3, CitaId = 106, FechaControl = Utc(2023, 10, 15), SemanaGestacion = 8, TipoSignoPrenatalId = 5, Valor = 90 },
                new ControlPrenatal { Id = 7, SeguimientoId = 3, CitaId = 107, FechaControl = Utc(2024, 1, 5), SemanaGestacion = 28, TipoSignoPrenatalId = 1, Valor = 120 },
                new ControlPrenatal { Id = 8, SeguimientoId = 4, CitaId = 108, FechaControl = Utc(2023, 11, 1), SemanaGestacion = 14, TipoSignoPrenatalId = 2, Valor = 75 },
                new ControlPrenatal { Id = 9, SeguimientoId = 4, CitaId = 109, FechaControl = Utc(2024, 2, 1), SemanaGestacion = 32, TipoSignoPrenatalId = 3, Valor = 72 },
                new ControlPrenatal { Id = 10, SeguimientoId = 5, CitaId = 110, FechaControl = Utc(2024, 1, 15), SemanaGestacion = 10, TipoSignoPrenatalId = 1, Valor = 108 }
            );

            // 7. ComplicacionMaterna
            modelBuilder.Entity<ComplicacionMaterna>().HasData(
                new ComplicacionMaterna { Id = 1, PartoId = 2, TipoComplicacion = "Hemorragia", Descripcion = "Sangrado post cesárea moderado", TratamientoAplicado = "Oxitocina IV y compresión uterina", FechaRegistro = Utc(2024, 2, 20, 15, 0) },
                new ComplicacionMaterna { Id = 2, PartoId = 5, TipoComplicacion = "Desgarro", Descripcion = "Desgarro perineal grado 1", TratamientoAplicado = "Sutura con anestesia local", FechaRegistro = Utc(2024, 5, 18, 8, 0) }
            );

            // 8. EvaluacionInicial
            modelBuilder.Entity<EvaluacionInicial>().HasData(
                new EvaluacionInicial { Id = 1, RecienNacidoId = 1, CIEmpleado = "1234567", ApgarMin1 = 8, ApgarMin5 = 9, FechaEvaluacion = Utc(2024, 1, 15, 8, 50) },
                new EvaluacionInicial { Id = 2, RecienNacidoId = 2, CIEmpleado = "2345678", ApgarMin1 = 6, ApgarMin5 = 7, FechaEvaluacion = Utc(2024, 2, 20, 14, 35) },
                new EvaluacionInicial { Id = 3, RecienNacidoId = 3, CIEmpleado = "1234567", ApgarMin1 = 9, ApgarMin5 = 10, FechaEvaluacion = Utc(2024, 3, 10, 22, 25) },
                new EvaluacionInicial { Id = 4, RecienNacidoId = 4, CIEmpleado = "3456789", ApgarMin1 = 5, ApgarMin5 = 6, FechaEvaluacion = Utc(2024, 4, 5, 12, 5) },
                new EvaluacionInicial { Id = 5, RecienNacidoId = 5, CIEmpleado = "2345678", ApgarMin1 = 8, ApgarMin5 = 9, FechaEvaluacion = Utc(2024, 5, 18, 7, 15) }
            );

            // 9. SignoVitalNeonatal
            modelBuilder.Entity<SignoVitalNeonatal>().HasData(
                new SignoVitalNeonatal { Id = 1, RecienNacidoId = 1, TipoSignoId = 1, CIEmpleado = "1234567", FechaHora = Utc(2024, 1, 15, 10, 0), Valor = 145 },
                new SignoVitalNeonatal { Id = 2, RecienNacidoId = 1, TipoSignoId = 2, CIEmpleado = "1234567", FechaHora = Utc(2024, 1, 15, 10, 0), Valor = 37.0m },
                new SignoVitalNeonatal { Id = 3, RecienNacidoId = 2, TipoSignoId = 3, CIEmpleado = "2345678", FechaHora = Utc(2024, 2, 20, 16, 0), Valor = 93 },
                new SignoVitalNeonatal { Id = 4, RecienNacidoId = 2, TipoSignoId = 1, CIEmpleado = "2345678", FechaHora = Utc(2024, 2, 20, 16, 0), Valor = 155 },
                new SignoVitalNeonatal { Id = 5, RecienNacidoId = 3, TipoSignoId = 2, CIEmpleado = "1234567", FechaHora = Utc(2024, 3, 10, 23, 0), Valor = 36.8m },
                new SignoVitalNeonatal { Id = 6, RecienNacidoId = 4, TipoSignoId = 3, CIEmpleado = "3456789", FechaHora = Utc(2024, 4, 5, 14, 0), Valor = 92 },
                new SignoVitalNeonatal { Id = 7, RecienNacidoId = 4, TipoSignoId = 4, CIEmpleado = "3456789", FechaHora = Utc(2024, 4, 5, 14, 0), Valor = 65 },
                new SignoVitalNeonatal { Id = 8, RecienNacidoId = 5, TipoSignoId = 1, CIEmpleado = "2345678", FechaHora = Utc(2024, 5, 18, 9, 0), Valor = 138 }
            );

            // 10. AlertaNeonatal
            modelBuilder.Entity<AlertaNeonatal>().HasData(
                new AlertaNeonatal { Id = 1, RecienNacidoId = 2, TipoSignoId = 3, TipoAlerta = "Moderada", Descripcion = "Saturación de oxígeno por debajo del rango normal", FechaGenerada = Utc(2024, 2, 20, 16, 5) },
                new AlertaNeonatal { Id = 2, RecienNacidoId = 4, TipoSignoId = 3, TipoAlerta = "Critica", Descripcion = "Saturación de oxígeno crítica, requiere intervención inmediata", FechaGenerada = Utc(2024, 4, 5, 14, 5) },
                new AlertaNeonatal { Id = 3, RecienNacidoId = 4, TipoSignoId = 4, TipoAlerta = "Moderada", Descripcion = "Frecuencia respiratoria elevada", FechaGenerada = Utc(2024, 4, 5, 14, 10) }
            );

            // 11. ComplicacionNeonatal
            modelBuilder.Entity<ComplicacionNeonatal>().HasData(
                new ComplicacionNeonatal { Id = 1, RecienNacidoId = 2, CIEmpleado = "2345678", TipoComplicacion = "Dificultad Respiratoria", Descripcion = "Síndrome de dificultad respiratoria leve", TratamientoAplicado = "Oxígeno suplementario por cánula nasal", FechaRegistro = Utc(2024, 2, 20, 17, 0) },
                new ComplicacionNeonatal { Id = 2, RecienNacidoId = 4, CIEmpleado = "3456789", TipoComplicacion = "Hipoxia", Descripcion = "Niveles bajos de oxígeno en sangre", TratamientoAplicado = "Ventilación asistida y monitoreo continuo", FechaRegistro = Utc(2024, 4, 5, 14, 30) }
            );

            // 12. TratamientoNeonatal
            modelBuilder.Entity<TratamientoNeonatal>().HasData(
                new TratamientoNeonatal { Id = 1, RecienNacidoId = 2, CIEmpleado = "2345678", CodigoMedicamento = "MED-001", TipoTratamiento = "Respiratorio", Descripcion = "Oxígeno suplementario por cánula nasal al 30%", FechaInicio = Utc(2024, 2, 20, 17, 0), FechaFin = Utc(2024, 2, 22, 10, 0) },
                new TratamientoNeonatal { Id = 2, RecienNacidoId = 4, CIEmpleado = "3456789", CodigoMedicamento = "MED-002", TipoTratamiento = "Ventilación", Descripcion = "Ventilación mecánica asistida", FechaInicio = Utc(2024, 4, 5, 14, 30), FechaFin = null },
                new TratamientoNeonatal { Id = 3, RecienNacidoId = 1, CIEmpleado = "1234567", CodigoMedicamento = "MED-003", TipoTratamiento = "Vitamínico", Descripcion = "Vitamina K profiláctica", FechaInicio = Utc(2024, 1, 15, 9, 0), FechaFin = Utc(2024, 1, 15, 9, 30) },
                new TratamientoNeonatal { Id = 4, RecienNacidoId = 3, CIEmpleado = "1234567", CodigoMedicamento = "MED-003", TipoTratamiento = "Vitamínico", Descripcion = "Vitamina K profiláctica", FechaInicio = Utc(2024, 3, 10, 22, 30), FechaFin = Utc(2024, 3, 10, 23, 0) },
                new TratamientoNeonatal { Id = 5, RecienNacidoId = 5, CIEmpleado = "2345678", CodigoMedicamento = "MED-003", TipoTratamiento = "Vitamínico", Descripcion = "Vitamina K profiláctica", FechaInicio = Utc(2024, 5, 18, 7, 30), FechaFin = Utc(2024, 5, 18, 8, 0) }
            );
        }
    }
}