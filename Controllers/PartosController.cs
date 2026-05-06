using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartosController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public PartosController(MaternidadContext context)
        {
            _context = context;
        }

        // GET: api/Partos
        [HttpGet]
        public async Task<IActionResult> GetPartos()
        {
            var data = await (
                from p in _context.Parto
                select new
                {
                    p.Id,
                    p.CIPaciente,
                    p.CIEmpleado,
                    p.FechaHora,
                    p.TipoParto,
                    p.Anestesia,
                    p.Complicaciones,
                    p.Observaciones,
                    Tiene_Complicacion = _context.ComplicacionMaterna.Any(c => c.PartoId == p.Id)
                }
            ).OrderByDescending(p => p.FechaHora).ToListAsync();

            return Ok(data);
        }

        // GET: api/Partos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParto(int id)
        {
            var data = await (
                from p in _context.Parto
                where p.Id == id
                select new
                {
                    p.Id,
                    p.CIPaciente,
                    p.CIEmpleado,
                    p.FechaHora,
                    p.TipoParto,
                    p.Anestesia,
                    p.Complicaciones,
                    p.Observaciones
                }
            ).FirstOrDefaultAsync();

            if (data == null)
                return NotFound(new { Mensaje = $"No se encontró parto con ID: {id}" });

            return Ok(data);
        }

        // GET: api/Partos/por-ci/{ci}
        // Buscar todos los partos de una madre por su CI
        [HttpGet("por-ci/{ci}")]
        public async Task<IActionResult> GetPartoPorCI(string ci)
        {
            var data = await (
                from p in _context.Parto
                where p.CIPaciente == ci
                orderby p.FechaHora descending
                select new
                {
                    p.Id,
                    p.CIPaciente,
                    p.CIEmpleado,
                    p.FechaHora,
                    p.TipoParto,
                    p.Anestesia,
                    p.Complicaciones,
                    p.Observaciones
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No se encontraron partos para CI: {ci}" });

            return Ok(data);
        }

        // POST: api/Partos
        [HttpPost]
        public async Task<IActionResult> PostParto([FromBody] Parto parto)
        {
            _context.Parto.Add(parto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParto), new { id = parto.Id }, new
            {
                Mensaje = "Parto registrado correctamente",
                parto.Id,
                parto.CIPaciente,
                parto.FechaHora,
                parto.TipoParto
            });
        }
    }
}