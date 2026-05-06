using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maternidad.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parto",
                columns: new[] { "Id", "Anestesia", "CIEmpleado", "CIPaciente", "Complicaciones", "FechaHora", "Observaciones", "TipoParto" },
                values: new object[,]
                {
                    { 1, "Epidural", "1234567", "7654321", "Ninguna", new DateTime(2024, 1, 15, 8, 30, 0, 0, DateTimeKind.Utc), "Parto sin complicaciones", "Natural" },
                    { 2, "General", "2345678", "8765432", "Sangrado leve", new DateTime(2024, 2, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Se requirió cesárea de emergencia", "Cesárea" },
                    { 3, "Ninguna", "1234567", "9876543", "Ninguna", new DateTime(2024, 3, 10, 22, 15, 0, 0, DateTimeKind.Utc), "Parto rápido sin intervención", "Natural" },
                    { 4, "Epidural", "3456789", "6543210", "Ninguna", new DateTime(2024, 4, 5, 11, 45, 0, 0, DateTimeKind.Utc), "Cesárea programada", "Cesárea" },
                    { 5, "Epidural", "2345678", "5432109", "Desgarro leve", new DateTime(2024, 5, 18, 7, 0, 0, 0, DateTimeKind.Utc), "Se realizó episiotomía", "Natural" }
                });

            migrationBuilder.InsertData(
                table: "TipoSignoPrenatal",
                columns: new[] { "Id", "Nombre", "Unidad", "ValorMax", "ValorMin" },
                values: new object[,]
                {
                    { 1, "Presión Arterial Sistólica", "mmHg", 140m, 90m },
                    { 2, "Frecuencia Cardíaca", "bpm", 100m, 60m },
                    { 3, "Peso", "kg", 120m, 45m },
                    { 4, "Altura Uterina", "cm", 38m, 20m },
                    { 5, "Glucosa", "mg/dL", 110m, 70m }
                });

            migrationBuilder.InsertData(
                table: "TipoSignoVitalNeonatal",
                columns: new[] { "Id", "Nombre", "Unidad", "ValorMax", "ValorMin" },
                values: new object[,]
                {
                    { 1, "Frecuencia Cardíaca", "bpm", 160m, 100m },
                    { 2, "Temperatura", "°C", 37.5m, 36.5m },
                    { 3, "Saturación de Oxígeno", "%", 100m, 95m },
                    { 4, "Frecuencia Respiratoria", "rpm", 60m, 30m },
                    { 5, "Glucosa Neonatal", "mg/dL", 125m, 45m }
                });

            migrationBuilder.InsertData(
                table: "ComplicacionMaterna",
                columns: new[] { "Id", "Descripcion", "FechaRegistro", "PartoId", "TipoComplicacion", "TratamientoAplicado" },
                values: new object[,]
                {
                    { 1, "Sangrado post cesárea moderado", new DateTime(2024, 2, 20, 15, 0, 0, 0, DateTimeKind.Utc), 2, "Hemorragia", "Oxitocina IV y compresión uterina" },
                    { 2, "Desgarro perineal grado 1", new DateTime(2024, 5, 18, 8, 0, 0, 0, DateTimeKind.Utc), 5, "Desgarro", "Sutura con anestesia local" }
                });

            migrationBuilder.InsertData(
                table: "RecienNacido",
                columns: new[] { "Id", "CIPaciente", "Estado", "FechaHoraNacimiento", "Nombre", "PartoId", "PesoGramos", "Sexo", "TallaCm" },
                values: new object[,]
                {
                    { 1, "7654321", "Sano", new DateTime(2024, 1, 15, 8, 45, 0, 0, DateTimeKind.Utc), "Bebé García", 1, 3200m, "Masculino", 50m },
                    { 2, "8765432", "En observación", new DateTime(2024, 2, 20, 14, 30, 0, 0, DateTimeKind.Utc), "Bebé Mamani", 2, 2900m, "Femenino", 48m },
                    { 3, "9876543", "Sano", new DateTime(2024, 3, 10, 22, 20, 0, 0, DateTimeKind.Utc), "Bebé Flores", 3, 3500m, "Masculino", 52m },
                    { 4, "6543210", "En observación", new DateTime(2024, 4, 5, 12, 0, 0, 0, DateTimeKind.Utc), "Bebé Quispe", 4, 2750m, "Femenino", 47m },
                    { 5, "5432109", "Sano", new DateTime(2024, 5, 18, 7, 10, 0, 0, DateTimeKind.Utc), "Bebé Chávez", 5, 3800m, "Masculino", 54m }
                });

            migrationBuilder.InsertData(
                table: "SeguimientoPrenatal",
                columns: new[] { "Id", "CIEmpleado", "CIPaciente", "Estado", "FechaInicio", "PartoId" },
                values: new object[,]
                {
                    { 1, "1234567", "7654321", "Completado", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, "2345678", "8765432", "Completado", new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, "1234567", "9876543", "Completado", new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 4, "3456789", "6543210", "Completado", new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 5, "2345678", "5432109", "Completado", new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5 }
                });

            migrationBuilder.InsertData(
                table: "AlertaNeonatal",
                columns: new[] { "Id", "Descripcion", "FechaGenerada", "RecienNacidoId", "TipoAlerta", "TipoSignoId" },
                values: new object[,]
                {
                    { 1, "Saturación de oxígeno por debajo del rango normal", new DateTime(2024, 2, 20, 16, 5, 0, 0, DateTimeKind.Utc), 2, "Moderada", 3 },
                    { 2, "Saturación de oxígeno crítica, requiere intervención inmediata", new DateTime(2024, 4, 5, 14, 5, 0, 0, DateTimeKind.Utc), 4, "Critica", 3 },
                    { 3, "Frecuencia respiratoria elevada", new DateTime(2024, 4, 5, 14, 10, 0, 0, DateTimeKind.Utc), 4, "Moderada", 4 }
                });

            migrationBuilder.InsertData(
                table: "ComplicacionNeonatal",
                columns: new[] { "Id", "CIEmpleado", "Descripcion", "FechaRegistro", "RecienNacidoId", "TipoComplicacion", "TratamientoAplicado" },
                values: new object[,]
                {
                    { 1, "2345678", "Síndrome de dificultad respiratoria leve", new DateTime(2024, 2, 20, 17, 0, 0, 0, DateTimeKind.Utc), 2, "Dificultad Respiratoria", "Oxígeno suplementario por cánula nasal" },
                    { 2, "3456789", "Niveles bajos de oxígeno en sangre", new DateTime(2024, 4, 5, 14, 30, 0, 0, DateTimeKind.Utc), 4, "Hipoxia", "Ventilación asistida y monitoreo continuo" }
                });

            migrationBuilder.InsertData(
                table: "ControlPrenatal",
                columns: new[] { "Id", "CitaId", "FechaControl", "SeguimientoId", "SemanaGestacion", "TipoSignoPrenatalId", "Valor" },
                values: new object[,]
                {
                    { 1, 101, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 12, 1, 110m },
                    { 2, 102, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, 24, 2, 80m },
                    { 3, 103, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), 1, 36, 3, 68m },
                    { 4, 104, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 10, 1, 115m },
                    { 5, 105, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 22, 4, 28m },
                    { 6, 106, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, 8, 5, 90m },
                    { 7, 107, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, 28, 1, 120m },
                    { 8, 108, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 14, 2, 75m },
                    { 9, 109, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 32, 3, 72m },
                    { 10, 110, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 10, 1, 108m }
                });

            migrationBuilder.InsertData(
                table: "EvaluacionInicial",
                columns: new[] { "Id", "ApgarMin1", "ApgarMin5", "CIEmpleado", "FechaEvaluacion", "RecienNacidoId" },
                values: new object[,]
                {
                    { 1, 8, 9, "1234567", new DateTime(2024, 1, 15, 8, 50, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 6, 7, "2345678", new DateTime(2024, 2, 20, 14, 35, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 9, 10, "1234567", new DateTime(2024, 3, 10, 22, 25, 0, 0, DateTimeKind.Utc), 3 },
                    { 4, 5, 6, "3456789", new DateTime(2024, 4, 5, 12, 5, 0, 0, DateTimeKind.Utc), 4 },
                    { 5, 8, 9, "2345678", new DateTime(2024, 5, 18, 7, 15, 0, 0, DateTimeKind.Utc), 5 }
                });

            migrationBuilder.InsertData(
                table: "SignoVitalNeonatal",
                columns: new[] { "Id", "CIEmpleado", "FechaHora", "RecienNacidoId", "TipoSignoId", "Valor" },
                values: new object[,]
                {
                    { 1, "1234567", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), 1, 1, 145m },
                    { 2, "1234567", new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), 1, 2, 37.0m },
                    { 3, "2345678", new DateTime(2024, 2, 20, 16, 0, 0, 0, DateTimeKind.Utc), 2, 3, 93m },
                    { 4, "2345678", new DateTime(2024, 2, 20, 16, 0, 0, 0, DateTimeKind.Utc), 2, 1, 155m },
                    { 5, "1234567", new DateTime(2024, 3, 10, 23, 0, 0, 0, DateTimeKind.Utc), 3, 2, 36.8m },
                    { 6, "3456789", new DateTime(2024, 4, 5, 14, 0, 0, 0, DateTimeKind.Utc), 4, 3, 92m },
                    { 7, "3456789", new DateTime(2024, 4, 5, 14, 0, 0, 0, DateTimeKind.Utc), 4, 4, 65m },
                    { 8, "2345678", new DateTime(2024, 5, 18, 9, 0, 0, 0, DateTimeKind.Utc), 5, 1, 138m }
                });

            migrationBuilder.InsertData(
                table: "TratamientoNeonatal",
                columns: new[] { "Id", "CIEmpleado", "CodigoMedicamento", "Descripcion", "FechaFin", "FechaInicio", "RecienNacidoId", "TipoTratamiento" },
                values: new object[,]
                {
                    { 1, "2345678", "MED-001", "Oxígeno suplementario por cánula nasal al 30%", new DateTime(2024, 2, 22, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 20, 17, 0, 0, 0, DateTimeKind.Utc), 2, "Respiratorio" },
                    { 2, "3456789", "MED-002", "Ventilación mecánica asistida", null, new DateTime(2024, 4, 5, 14, 30, 0, 0, DateTimeKind.Utc), 4, "Ventilación" },
                    { 3, "1234567", "MED-003", "Vitamina K profiláctica", new DateTime(2024, 1, 15, 9, 30, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 15, 9, 0, 0, 0, DateTimeKind.Utc), 1, "Vitamínico" },
                    { 4, "1234567", "MED-003", "Vitamina K profiláctica", new DateTime(2024, 3, 10, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 10, 22, 30, 0, 0, DateTimeKind.Utc), 3, "Vitamínico" },
                    { 5, "2345678", "MED-003", "Vitamina K profiláctica", new DateTime(2024, 5, 18, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 18, 7, 30, 0, 0, DateTimeKind.Utc), 5, "Vitamínico" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlertaNeonatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AlertaNeonatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AlertaNeonatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComplicacionMaterna",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComplicacionMaterna",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComplicacionNeonatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComplicacionNeonatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ControlPrenatal",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EvaluacionInicial",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EvaluacionInicial",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EvaluacionInicial",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EvaluacionInicial",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EvaluacionInicial",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TipoSignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TratamientoNeonatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TratamientoNeonatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TratamientoNeonatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TratamientoNeonatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TratamientoNeonatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RecienNacido",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RecienNacido",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RecienNacido",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RecienNacido",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RecienNacido",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SeguimientoPrenatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeguimientoPrenatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SeguimientoPrenatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SeguimientoPrenatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SeguimientoPrenatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TipoSignoPrenatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoSignoPrenatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoSignoPrenatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoSignoPrenatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoSignoPrenatal",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TipoSignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoSignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoSignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoSignoVitalNeonatal",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parto",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parto",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
