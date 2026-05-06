using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Maternidad.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CIPaciente = table.Column<string>(type: "text", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoParto = table.Column<string>(type: "text", nullable: false),
                    Anestesia = table.Column<string>(type: "text", nullable: false),
                    Complicaciones = table.Column<string>(type: "text", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSignoPrenatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Unidad = table.Column<string>(type: "text", nullable: false),
                    ValorMin = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorMax = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSignoPrenatal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSignoVitalNeonatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Unidad = table.Column<string>(type: "text", nullable: false),
                    ValorMin = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorMax = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSignoVitalNeonatal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplicacionMaterna",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartoId = table.Column<int>(type: "integer", nullable: false),
                    TipoComplicacion = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    TratamientoAplicado = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplicacionMaterna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplicacionMaterna_Parto_PartoId",
                        column: x => x.PartoId,
                        principalTable: "Parto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecienNacido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartoId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CIPaciente = table.Column<string>(type: "text", nullable: false),
                    FechaHoraNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PesoGramos = table.Column<decimal>(type: "numeric", nullable: false),
                    TallaCm = table.Column<decimal>(type: "numeric", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecienNacido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecienNacido_Parto_PartoId",
                        column: x => x.PartoId,
                        principalTable: "Parto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoPrenatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CIPaciente = table.Column<string>(type: "text", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    PartoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoPrenatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeguimientoPrenatal_Parto_PartoId",
                        column: x => x.PartoId,
                        principalTable: "Parto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlertaNeonatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecienNacidoId = table.Column<int>(type: "integer", nullable: false),
                    TipoSignoId = table.Column<int>(type: "integer", nullable: false),
                    TipoAlerta = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaGenerada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertaNeonatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertaNeonatal_RecienNacido_RecienNacidoId",
                        column: x => x.RecienNacidoId,
                        principalTable: "RecienNacido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertaNeonatal_TipoSignoVitalNeonatal_TipoSignoId",
                        column: x => x.TipoSignoId,
                        principalTable: "TipoSignoVitalNeonatal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplicacionNeonatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecienNacidoId = table.Column<int>(type: "integer", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    TipoComplicacion = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    TratamientoAplicado = table.Column<string>(type: "text", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplicacionNeonatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplicacionNeonatal_RecienNacido_RecienNacidoId",
                        column: x => x.RecienNacidoId,
                        principalTable: "RecienNacido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionInicial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecienNacidoId = table.Column<int>(type: "integer", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    ApgarMin1 = table.Column<int>(type: "integer", nullable: false),
                    ApgarMin5 = table.Column<int>(type: "integer", nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionInicial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionInicial_RecienNacido_RecienNacidoId",
                        column: x => x.RecienNacidoId,
                        principalTable: "RecienNacido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignoVitalNeonatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecienNacidoId = table.Column<int>(type: "integer", nullable: false),
                    TipoSignoId = table.Column<int>(type: "integer", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignoVitalNeonatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignoVitalNeonatal_RecienNacido_RecienNacidoId",
                        column: x => x.RecienNacidoId,
                        principalTable: "RecienNacido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignoVitalNeonatal_TipoSignoVitalNeonatal_TipoSignoId",
                        column: x => x.TipoSignoId,
                        principalTable: "TipoSignoVitalNeonatal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TratamientoNeonatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecienNacidoId = table.Column<int>(type: "integer", nullable: false),
                    CIEmpleado = table.Column<string>(type: "text", nullable: false),
                    CodigoMedicamento = table.Column<string>(type: "text", nullable: false),
                    TipoTratamiento = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamientoNeonatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamientoNeonatal_RecienNacido_RecienNacidoId",
                        column: x => x.RecienNacidoId,
                        principalTable: "RecienNacido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlPrenatal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeguimientoId = table.Column<int>(type: "integer", nullable: false),
                    CitaId = table.Column<int>(type: "integer", nullable: false),
                    FechaControl = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SemanaGestacion = table.Column<int>(type: "integer", nullable: false),
                    TipoSignoPrenatalId = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPrenatal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlPrenatal_SeguimientoPrenatal_SeguimientoId",
                        column: x => x.SeguimientoId,
                        principalTable: "SeguimientoPrenatal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlPrenatal_TipoSignoPrenatal_TipoSignoPrenatalId",
                        column: x => x.TipoSignoPrenatalId,
                        principalTable: "TipoSignoPrenatal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertaNeonatal_RecienNacidoId",
                table: "AlertaNeonatal",
                column: "RecienNacidoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertaNeonatal_TipoSignoId",
                table: "AlertaNeonatal",
                column: "TipoSignoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplicacionMaterna_PartoId",
                table: "ComplicacionMaterna",
                column: "PartoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplicacionNeonatal_RecienNacidoId",
                table: "ComplicacionNeonatal",
                column: "RecienNacidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPrenatal_SeguimientoId",
                table: "ControlPrenatal",
                column: "SeguimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPrenatal_TipoSignoPrenatalId",
                table: "ControlPrenatal",
                column: "TipoSignoPrenatalId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionInicial_RecienNacidoId",
                table: "EvaluacionInicial",
                column: "RecienNacidoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecienNacido_PartoId",
                table: "RecienNacido",
                column: "PartoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoPrenatal_PartoId",
                table: "SeguimientoPrenatal",
                column: "PartoId");

            migrationBuilder.CreateIndex(
                name: "IX_SignoVitalNeonatal_RecienNacidoId",
                table: "SignoVitalNeonatal",
                column: "RecienNacidoId");

            migrationBuilder.CreateIndex(
                name: "IX_SignoVitalNeonatal_TipoSignoId",
                table: "SignoVitalNeonatal",
                column: "TipoSignoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientoNeonatal_RecienNacidoId",
                table: "TratamientoNeonatal",
                column: "RecienNacidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertaNeonatal");

            migrationBuilder.DropTable(
                name: "ComplicacionMaterna");

            migrationBuilder.DropTable(
                name: "ComplicacionNeonatal");

            migrationBuilder.DropTable(
                name: "ControlPrenatal");

            migrationBuilder.DropTable(
                name: "EvaluacionInicial");

            migrationBuilder.DropTable(
                name: "SignoVitalNeonatal");

            migrationBuilder.DropTable(
                name: "TratamientoNeonatal");

            migrationBuilder.DropTable(
                name: "SeguimientoPrenatal");

            migrationBuilder.DropTable(
                name: "TipoSignoPrenatal");

            migrationBuilder.DropTable(
                name: "TipoSignoVitalNeonatal");

            migrationBuilder.DropTable(
                name: "RecienNacido");

            migrationBuilder.DropTable(
                name: "Parto");
        }
    }
}
