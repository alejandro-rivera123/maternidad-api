using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public ConsultasController(MaternidadContext context)
        {
            _context = context;
        }

        // Lista todos los partos con su recién nacido
        // GET: api/Consultas/RecienNacidos
        [HttpGet("RecienNacidos")]
        public async Task<IActionResult> PartosConRecienNacido()
        {
            var data = await _context.Parto
                .Select(p => new
                {
                    p.CIPaciente,
                    p.CIEmpleado,
                    Fecha_Parto = p.FechaHora,
                    Tipo_Parto = p.TipoParto,
                    Bebe_Nombre = p.RecienNacido.Nombre,
                    Bebe_Peso_g = p.RecienNacido.PesoGramos,
                    Bebe_Talla_cm = p.RecienNacido.TallaCm,
                    Bebe_Sexo = p.RecienNacido.Sexo,
                    Bebe_Estado = p.RecienNacido.Estado
                })
                .ToListAsync();

            return Ok(data);
        }

        // Cantidad de partos por tipo
        // GET: api/Consultas/Conteo
        [HttpGet("Conteo")]
        public async Task<IActionResult> ConteoPorTipoParto()
        {
            var data = await _context.Parto
                .GroupBy(p => p.TipoParto)
                .Select(g => new
                {
                    Tipo_Parto = g.Key,
                    Total_Partos = g.Count()
                })
                .OrderByDescending(x => x.Total_Partos)
                .ToListAsync();

            return Ok(data);
        }

        // Peso promedio y total de recién nacidos por tipo de parto
        // GET: api/Consultas/PesoPorTipoParto
        [HttpGet("PesoPorTipoParto")]
        public async Task<IActionResult> PesoTotalPorTipoParto()
        {
            var data = await _context.RecienNacido
                .GroupBy(rn => rn.Parto.TipoParto)
                .Select(g => new
                {
                    Tipo_Parto = g.Key,
                    Cantidad_Bebes = g.Count(),
                    Peso_Total_Gramos = g.Sum(rn => rn.PesoGramos),
                    Peso_Promedio_Gramos = g.Average(rn => rn.PesoGramos)
                })
                .OrderByDescending(x => x.Peso_Total_Gramos)
                .ToListAsync();

            return Ok(data);
        }

        // Seguimiento prenatal por CI de paciente
        // GET: api/Consultas/SeguimientoPorCI/{ci}
        [HttpGet("SeguimientoPorCI/{ci}")]
        public async Task<IActionResult> SeguimientoPorCI(string ci)
        {
            var data = await _context.SeguimientoPrenatal
                .Where(s => s.CIPaciente == ci)
                .Select(s => new
                {
                    s.CIPaciente,
                    s.CIEmpleado,
                    s.FechaInicio,
                    s.Estado,
                    Total_Controles = s.Controles.Count,
                    Controles = s.Controles
                        .OrderBy(c => c.FechaControl)
                        .Select(c => new
                        {
                            c.FechaControl,
                            c.SemanaGestacion,
                            Tipo_Signo = c.TipoSigno.Nombre,
                            Unidad = c.TipoSigno.Unidad,
                            c.Valor
                        })
                        .ToList()
                })
                .ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No se encontró seguimiento para CI: {ci}" });

            return Ok(data);
        }

        // Partos sin complicación materna
        // GET: api/Consultas/PartosSinComplicacion
        [HttpGet("PartosSinComplicacion")]
        public async Task<IActionResult> PartosSinComplicacion()
        {
            var data = await _context.Parto
                .Where(p => !_context.ComplicacionMaterna.Any(c => c.PartoId == p.Id))
                .Select(p => new
                {
                    p.CIPaciente,
                    p.CIEmpleado,
                    Fecha_Parto = p.FechaHora,
                    Tipo_Parto = p.TipoParto,
                    p.Observaciones
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}