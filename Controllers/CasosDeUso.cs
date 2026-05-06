using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasosDeUso : ControllerBase
    {
        private readonly MaternidadContext _context;

        public CasosDeUso(MaternidadContext context)
        {
            _context = context;
        }

        // =============================================
        // CASOS DE USO DEL MÉDICO
        // =============================================

        // CU-01: Consultar historial de partos
        // GET: api/CasosDeUso/historial-partos
        [HttpGet("historial-partos")]
        public async Task<IActionResult> ConsultarHistorialPartos()
        {
            var data = await _context.Parto
                .OrderByDescending(p => p.FechaHora)
                .Select(p => new
                {
                    p.CIPaciente,
                    p.CIEmpleado,
                    p.FechaHora,
                    p.TipoParto,
                    p.Anestesia,
                    p.Complicaciones,
                    p.Observaciones,
                    Tiene_Complicacion = _context.ComplicacionMaterna.Any(c => c.PartoId == p.Id)
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-02: Consultar seguimiento prenatal
        // GET: api/CasosDeUso/seguimiento-prenatal
        [HttpGet("seguimiento-prenatal")]
        public async Task<IActionResult> ConsultarSeguimientoPrenatal()
        {
            var data = await _context.SeguimientoPrenatal
                .Select(s => new
                {
                    s.CIPaciente,
                    s.CIEmpleado,
                    s.FechaInicio,
                    s.Estado,
                    Total_Controles = s.Controles.Count,
                    Ultimo_Control = s.Controles
                        .OrderByDescending(c => c.FechaControl)
                        .Select(c => c.FechaControl)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-03: Consultar complicaciones maternas
        // GET: api/CasosDeUso/complicaciones-maternas
        [HttpGet("complicaciones-maternas")]
        public async Task<IActionResult> ConsultarComplicacionesMaternas()
        {
            var data = await _context.ComplicacionMaterna
                .OrderByDescending(cm => cm.FechaRegistro)
                .Select(cm => new
                {
                    cm.Parto.CIPaciente,
                    cm.Parto.CIEmpleado,
                    Fecha_Parto = cm.Parto.FechaHora,
                    Tipo_Parto = cm.Parto.TipoParto,
                    cm.TipoComplicacion,
                    cm.Descripcion,
                    cm.TratamientoAplicado,
                    cm.FechaRegistro
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-04: Consultar tratamientos neonatales
        // GET: api/CasosDeUso/tratamientos-neonatales
        [HttpGet("tratamientos-neonatales")]
        public async Task<IActionResult> ConsultarTratamientosNeonatales()
        {
            var data = await _context.TratamientoNeonatal
                .OrderByDescending(t => t.FechaInicio)
                .Select(t => new
                {
                    RecienNacido = t.RecienNacido.Nombre,
                    t.RecienNacido.Estado,
                    Fecha_Nacimiento = t.RecienNacido.FechaHoraNacimiento,
                    t.CIEmpleado,
                    t.CodigoMedicamento,
                    t.TipoTratamiento,
                    t.Descripcion,
                    t.FechaInicio,
                    t.FechaFin,
                    Estado_Tratamiento = t.FechaFin == null ? "En curso" : "Finalizado"
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-05: Consultar evaluación inicial neonatal
        // GET: api/CasosDeUso/evaluacion-inicial-neonatal
        [HttpGet("evaluacion-inicial-neonatal")]
        public async Task<IActionResult> ConsultarEvaluacionInicialNeonatal()
        {
            var data = await _context.EvaluacionInicial
                .Select(e => new
                {
                    RecienNacido = e.RecienNacido.Nombre,
                    e.RecienNacido.PesoGramos,
                    e.RecienNacido.TallaCm,
                    e.RecienNacido.Sexo,
                    e.CIEmpleado,
                    e.ApgarMin1,
                    e.ApgarMin5,
                    e.FechaEvaluacion,
                    Resultado = e.ApgarMin5 >= 7 ? "Normal" :
                                e.ApgarMin5 >= 4 ? "Depresion Moderada" : "Depresion Severa",
                    Tipo_Parto = e.RecienNacido.Parto.TipoParto
                })
                .ToListAsync();

            return Ok(data);
        }

        // =============================================
        // CASOS DE USO DE LA ENFERMERA
        // =============================================

        // CU-06: Consultar estado postparto
        // GET: api/CasosDeUso/estado-postparto
        [HttpGet("estado-postparto")]
        public async Task<IActionResult> ConsultarEstadoPostparto()
        {
            var data = await _context.Parto
                .OrderByDescending(p => p.FechaHora)
                .Select(p => new
                {
                    p.CIPaciente,
                    Fecha_Parto = p.FechaHora,
                    Tipo_Parto = p.TipoParto,
                    p.Complicaciones,
                    Bebe_Nombre = p.RecienNacido.Nombre,
                    Bebe_Estado = p.RecienNacido.Estado,
                    Tuvo_Complicacion_Materna = _context.ComplicacionMaterna.Any(c => c.PartoId == p.Id)
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-07: Consultar signos vitales neonatales
        // GET: api/CasosDeUso/signos-vitales-neonatales
        [HttpGet("signos-vitales-neonatales")]
        public async Task<IActionResult> ConsultarSignosVitalesNeonatales()
        {
            var data = await _context.SignoVitalNeonatal
                .OrderByDescending(sv => sv.FechaHora)
                .Select(sv => new
                {
                    RecienNacido = sv.RecienNacido.Nombre,
                    sv.RecienNacido.Estado,
                    sv.FechaHora,
                    Tipo_Signo = sv.TipoSigno.Nombre,
                    sv.TipoSigno.Unidad,
                    sv.Valor,
                    Rango_Normal = sv.TipoSigno.ValorMin + " - " + sv.TipoSigno.ValorMax,
                    Fuera_de_Rango = sv.Valor < sv.TipoSigno.ValorMin || sv.Valor > sv.TipoSigno.ValorMax,
                    sv.CIEmpleado
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-08: Consultar alertas neonatales activas
        // GET: api/CasosDeUso/alertas-neonatales-activas
        [HttpGet("alertas-neonatales-activas")]
        public async Task<IActionResult> ConsultarAlertasNeonatalesActivas()
        {
            var data = await _context.AlertaNeonatal
                .Select(a => new
                {
                    RecienNacido = a.RecienNacido.Nombre,
                    a.RecienNacido.Estado,
                    Tipo_Signo = a.TipoSigno.Nombre,
                    a.TipoAlerta,
                    a.Descripcion,
                    a.FechaGenerada,
                    Prioridad = a.TipoAlerta == "Critica" ? 1 :
                                a.TipoAlerta == "Moderada" ? 2 : 3
                })
                .OrderBy(x => x.Prioridad)
                .ThenByDescending(x => x.FechaGenerada)
                .ToListAsync();

            return Ok(data);
        }

        // CU-09: Consultar complicaciones neonatales
        // GET: api/CasosDeUso/complicaciones-neonatales
        [HttpGet("complicaciones-neonatales")]
        public async Task<IActionResult> ConsultarComplicacionesNeonatales()
        {
            var data = await _context.ComplicacionNeonatal
                .OrderByDescending(cn => cn.FechaRegistro)
                .Select(cn => new
                {
                    RecienNacido = cn.RecienNacido.Nombre,
                    cn.RecienNacido.Estado,
                    CI_Madre = cn.RecienNacido.CIPaciente,
                    cn.CIEmpleado,
                    cn.TipoComplicacion,
                    cn.Descripcion,
                    cn.TratamientoAplicado,
                    cn.FechaRegistro
                })
                .ToListAsync();

            return Ok(data);
        }

        // CU-10: Consultar recién nacidos por fecha (turno)
        // GET: api/CasosDeUso/recien-nacidos-por-turno/2024-01-15
        [HttpGet("recien-nacidos-por-turno/{fecha}")]
        public async Task<IActionResult> ConsultarRecienNacidosPorTurno(DateTime fecha)
        {
            var data = await _context.RecienNacido
                .Where(rn => rn.FechaHoraNacimiento.Date == fecha.Date)
                .OrderBy(rn => rn.FechaHoraNacimiento)
                .Select(rn => new
                {
                    rn.Nombre,
                    rn.FechaHoraNacimiento,
                    rn.PesoGramos,
                    rn.TallaCm,
                    rn.Sexo,
                    rn.Estado,
                    CI_Madre = rn.CIPaciente,
                    Tipo_Parto = rn.Parto.TipoParto,
                    Tiene_Alerta = rn.Alertas.Any()
                })
                .ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No hay recién nacidos para la fecha: {fecha:yyyy-MM-dd}" });

            return Ok(new
            {
                Fecha = fecha.ToString("yyyy-MM-dd"),
                Total = data.Count,
                RecienNacidos = data
            });
        }
    }
}