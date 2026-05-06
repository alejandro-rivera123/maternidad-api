using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlPrenatalController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public ControlPrenatalController(MaternidadContext context)
        {
            _context = context;
        }

        // GET: api/ControlPrenatal
        [HttpGet]
        public async Task<IActionResult> GetControles()
        {
            var data = await (
                from c in _context.ControlPrenatal
                join s in _context.SeguimientoPrenatal on c.SeguimientoId equals s.Id
                join ts in _context.TipoSignoPrenatal on c.TipoSignoPrenatalId equals ts.Id
                orderby c.FechaControl
                select new
                {
                    c.Id,
                    CIPaciente = s.CIPaciente,
                    CIEmpleado = s.CIEmpleado,
                    c.CitaId,
                    c.FechaControl,
                    c.SemanaGestacion,
                    Tipo_Signo = ts.Nombre,
                    ts.Unidad,
                    c.Valor,
                    Rango_Normal = ts.ValorMin + " - " + ts.ValorMax,
                    Fuera_de_Rango = c.Valor < ts.ValorMin || c.Valor > ts.ValorMax
                }
            ).ToListAsync();

            return Ok(data);
        }

        // GET: api/ControlPrenatal/por-seguimiento/{seguimientoId}
        [HttpGet("por-seguimiento/{seguimientoId}")]
        public async Task<IActionResult> GetPorSeguimiento(int seguimientoId)
        {
            var data = await (
                from c in _context.ControlPrenatal
                join ts in _context.TipoSignoPrenatal on c.TipoSignoPrenatalId equals ts.Id
                where c.SeguimientoId == seguimientoId
                orderby c.FechaControl
                select new
                {
                    c.Id,
                    c.CitaId,
                    c.FechaControl,
                    c.SemanaGestacion,
                    Tipo_Signo = ts.Nombre,
                    ts.Unidad,
                    c.Valor,
                    ValorMin = ts.ValorMin,
                    ValorMax = ts.ValorMax,
                    Fuera_de_Rango = c.Valor < ts.ValorMin || c.Valor > ts.ValorMax
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No se encontraron controles para seguimiento ID: {seguimientoId}" });

            return Ok(data);
        }

        // GET: api/ControlPrenatal/por-cita/{citaId}
        // Buscar control por ID de cita (viene de Gestión de Turnos - Franz/Jonathan)
        [HttpGet("por-cita/{citaId}")]
        public async Task<IActionResult> GetPorCita(int citaId)
        {
            var data = await (
                from c in _context.ControlPrenatal
                join s in _context.SeguimientoPrenatal on c.SeguimientoId equals s.Id
                join ts in _context.TipoSignoPrenatal on c.TipoSignoPrenatalId equals ts.Id
                where c.CitaId == citaId
                select new
                {
                    c.Id,
                    c.CitaId,
                    CIPaciente = s.CIPaciente,
                    c.FechaControl,
                    c.SemanaGestacion,
                    Tipo_Signo = ts.Nombre,
                    ts.Unidad,
                    c.Valor,
                    Fuera_de_Rango = c.Valor < ts.ValorMin || c.Valor > ts.ValorMax
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No se encontraron controles para cita ID: {citaId}" });

            return Ok(data);
        }

        // POST: api/ControlPrenatal
        [HttpPost]
        public async Task<IActionResult> PostControl([FromBody] ControlPrenatal control)
        {
            var seguimiento = await _context.SeguimientoPrenatal.FindAsync(control.SeguimientoId);
            if (seguimiento == null)
                return NotFound(new { Mensaje = "Seguimiento prenatal no encontrado" });

            var tipoSigno = await _context.TipoSignoPrenatal.FindAsync(control.TipoSignoPrenatalId);
            if (tipoSigno == null)
                return NotFound(new { Mensaje = "Tipo de signo prenatal no encontrado" });

            _context.ControlPrenatal.Add(control);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Control prenatal registrado",
                control.Id,
                control.CitaId,
                control.FechaControl,
                control.SemanaGestacion,
                Tipo_Signo = tipoSigno.Nombre,
                control.Valor,
                Fuera_de_Rango = control.Valor < tipoSigno.ValorMin || control.Valor > tipoSigno.ValorMax
            });
        }
    }
}