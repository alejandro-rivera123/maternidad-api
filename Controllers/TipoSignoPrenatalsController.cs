using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoSignoPrenatalsController : ControllerBase
    {
        private readonly MaternidadContext _context;

        public TipoSignoPrenatalsController(MaternidadContext context)
        {
            _context = context;
        }

        // GET: api/TipoSignoPrenatals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSignoPrenatal>>> GetTipoSignoPrenatal()
        {
            return await _context.TipoSignoPrenatal.ToListAsync();
        }

        // GET: api/TipoSignoPrenatals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSignoPrenatal>> GetTipoSignoPrenatal(int id)
        {
            var tipoSignoPrenatal = await _context.TipoSignoPrenatal.FindAsync(id);
            if (tipoSignoPrenatal == null)
                return NotFound();

            return tipoSignoPrenatal;
        }

        // PUT: api/TipoSignoPrenatals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoSignoPrenatal(int id, TipoSignoPrenatal tipoSignoPrenatal)
        {
            if (id != tipoSignoPrenatal.Id)
                return BadRequest();

            _context.Entry(tipoSignoPrenatal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoSignoPrenatalExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/TipoSignoPrenatals
        [HttpPost]
        public async Task<ActionResult<TipoSignoPrenatal>> PostTipoSignoPrenatal(TipoSignoPrenatal tipoSignoPrenatal)
        {
            _context.TipoSignoPrenatal.Add(tipoSignoPrenatal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipoSignoPrenatal), new { id = tipoSignoPrenatal.Id }, tipoSignoPrenatal);
        }

        // DELETE: api/TipoSignoPrenatals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoSignoPrenatal(int id)
        {
            var tipoSignoPrenatal = await _context.TipoSignoPrenatal.FindAsync(id);
            if (tipoSignoPrenatal == null)
                return NotFound();

            _context.TipoSignoPrenatal.Remove(tipoSignoPrenatal);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TipoSignoPrenatalExists(int id)
        {
            return _context.TipoSignoPrenatal.Any(e => e.Id == id);
        }
    }
}