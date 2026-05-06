using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecienNacidosController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public RecienNacidosController(MaternidadContext context)
        {
            _context = context;
        }

        // GET: api/RecienNacidos
        [HttpGet]
        public async Task<IActionResult> GetRecienNacidos()
        {
            var data = await (
                from rn in _context.RecienNacido
                join p in _context.Parto on rn.PartoId equals p.Id
                select new
                {
                    rn.Id,
                    rn.Nombre,
                    rn.CIPaciente,
                    rn.FechaHoraNacimiento,
                    rn.PesoGramos,
                    rn.TallaCm,
                    rn.Sexo,
                    rn.Estado,
                    Tipo_Parto = p.TipoParto,
                    CIEmpleado = p.CIEmpleado,
                    Tiene_Alerta = _context.AlertaNeonatal.Any(a => a.RecienNacidoId == rn.Id)
                }
            ).ToListAsync();

            return Ok(data);
        }

        // GET: api/RecienNacidos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecienNacido(int id)
        {
            var rn = await _context.RecienNacido
                .Include(r => r.SignosVitales).ThenInclude(sv => sv.TipoSigno)
                .Include(r => r.Alertas).ThenInclude(a => a.TipoSigno)
                .Include(r => r.Tratamientos)
                .Include(r => r.Complicaciones)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rn == null)
                return NotFound(new { Mensaje = $"No se encontró recién nacido con ID: {id}" });

            return Ok(new
            {
                rn.Id,
                rn.Nombre,
                rn.CIPaciente,
                rn.FechaHoraNacimiento,
                rn.PesoGramos,
                rn.TallaCm,
                rn.Sexo,
                rn.Estado,
                Signos_Vitales = rn.SignosVitales.Select(sv => new
                {
                    sv.FechaHora,
                    Tipo = sv.TipoSigno.Nombre,
                    sv.TipoSigno.Unidad,
                    sv.Valor,
                    Fuera_de_Rango = sv.Valor < sv.TipoSigno.ValorMin || sv.Valor > sv.TipoSigno.ValorMax
                }),
                Alertas = rn.Alertas.Select(a => new
                {
                    a.TipoAlerta,
                    Tipo_Signo = a.TipoSigno.Nombre,
                    a.Descripcion,
                    a.FechaGenerada
                }),
                Tratamientos = rn.Tratamientos.Select(t => new
                {
                    t.TipoTratamiento,
                    t.Descripcion,
                    t.CIEmpleado,
                    t.CodigoMedicamento,
                    t.FechaInicio,
                    t.FechaFin,
                    Estado = t.FechaFin == null ? "En curso" : "Finalizado"
                }),
                Complicaciones = rn.Complicaciones.Select(c => new
                {
                    c.TipoComplicacion,
                    c.Descripcion,
                    c.TratamientoAplicado,
                    c.FechaRegistro
                })
            });
        }

        // GET: api/RecienNacidos/estado/{estado}
        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetPorEstado(string estado)
        {
            var data = await (
                from rn in _context.RecienNacido
                join p in _context.Parto on rn.PartoId equals p.Id
                where rn.Estado == estado
                select new
                {
                    rn.Id,
                    rn.Nombre,
                    rn.CIPaciente,
                    rn.Estado,
                    rn.FechaHoraNacimiento,
                    rn.PesoGramos,
                    Tipo_Parto = p.TipoParto
                }
            ).ToListAsync();

            return Ok(data);
        }

        // POST: api/RecienNacidos
        [HttpPost]
        public async Task<IActionResult> PostRecienNacido([FromBody] RecienNacido recienNacido)
        {
            var parto = await _context.Parto.FindAsync(recienNacido.PartoId);
            if (parto == null)
                return NotFound(new { Mensaje = "Parto no encontrado" });

            _context.RecienNacido.Add(recienNacido);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensaje = "Recién nacido registrado correctamente",
                recienNacido.Id,
                recienNacido.Nombre,
                recienNacido.CIPaciente,
                recienNacido.Estado
            });
        }
    }
}