using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoPrenatalsController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public SeguimientoPrenatalsController(MaternidadContext context)
        {
            _context = context;
        }

        // GET: api/SeguimientoPrenatals
        [HttpGet]
        public async Task<IActionResult> GetSeguimientoPrenatal()
        {
            var data = await (
                from s in _context.SeguimientoPrenatal
                select new
                {

                    s.CIPaciente,
                    s.CIEmpleado,
                    s.FechaInicio,
                    s.Estado,
                    Total_Controles = _context.ControlPrenatal.Count(c => c.SeguimientoId == s.Id),
                    Ultimo_Control = _context.ControlPrenatal
                                        .Where(c => c.SeguimientoId == s.Id)
                                        .OrderByDescending(c => c.FechaControl)
                                        .Select(c => c.FechaControl)
                                        .FirstOrDefault()
                }
            ).ToListAsync();

            return Ok(data);
        }

        // GET: api/SeguimientoPrenatals/por-ci/{ci}
        // Buscar seguimientos por CI del paciente
        [HttpGet("por-ci/{ci}")]
        public async Task<IActionResult> GetPorCI(string ci)
        {
            var data = await (
                from s in _context.SeguimientoPrenatal
                where s.CIPaciente == ci
                select new
                {
                    s.CIPaciente,
                    s.CIEmpleado,
                    s.FechaInicio,
                    s.Estado,
                    Total_Controles = _context.ControlPrenatal.Count(c => c.SeguimientoId == s.Id)
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No se encontraron seguimientos para CI: {ci}" });

            return Ok(data);
        }

        // POST: api/SeguimientoPrenatals
        [HttpPost]
        public async Task<IActionResult> PostSeguimientoPrenatal([FromBody] SeguimientoPrenatal seguimiento)
        {
            seguimiento.FechaInicio = DateTime.UtcNow;
            seguimiento.Estado = "Activo";

            _context.SeguimientoPrenatal.Add(seguimiento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Seguimiento prenatal registrado",
 
                seguimiento.CIPaciente,
                seguimiento.FechaInicio,
                seguimiento.Estado
            });
        }
    }
}