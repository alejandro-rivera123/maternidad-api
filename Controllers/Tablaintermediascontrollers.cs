using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maternidad.Data;
using Maternidad.Dominio;

// =============================================
// EVALUACION INICIAL
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionInicialController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public EvaluacionInicialController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetEvaluaciones()
        {
            var data = await (
                from e in _context.EvaluacionInicial
                join rn in _context.RecienNacido on e.RecienNacidoId equals rn.Id
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    e.CIEmpleado,
                    e.ApgarMin1,
                    e.ApgarMin5,
                    e.FechaEvaluacion,
                    Resultado = e.ApgarMin5 >= 7 ? "Normal" :
                                e.ApgarMin5 >= 4 ? "Depresion Moderada" : "Depresion Severa"
                }
            ).ToListAsync();
            return Ok(data);
        }

        [HttpGet("por-nombre/{nombre}")]
        public async Task<IActionResult> GetEvaluacionPorNombre(string nombre)
        {
            var data = await (
                from e in _context.EvaluacionInicial
                join rn in _context.RecienNacido on e.RecienNacidoId equals rn.Id
                where rn.Nombre.Contains(nombre)
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    rn.PesoGramos,
                    rn.TallaCm,
                    e.CIEmpleado,
                    e.ApgarMin1,
                    e.ApgarMin5,
                    e.FechaEvaluacion,
                    Resultado = e.ApgarMin5 >= 7 ? "Normal" :
                                e.ApgarMin5 >= 4 ? "Depresion Moderada" : "Depresion Severa"
                }
            ).FirstOrDefaultAsync();

            if (data == null)
                return NotFound(new { Mensaje = $"No se encontró evaluación para: {nombre}" });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostEvaluacion([FromBody] EvaluacionInicial evaluacion)
        {
            var rn = await _context.RecienNacido.FindAsync(evaluacion.RecienNacidoId);
            if (rn == null)
                return NotFound(new { Mensaje = "Recién nacido no encontrado" });

            var yaExiste = await _context.EvaluacionInicial
                .AnyAsync(e => e.RecienNacidoId == evaluacion.RecienNacidoId);
            if (yaExiste)
                return Conflict(new { Mensaje = "Ya existe una evaluación para este recién nacido" });

            _context.EvaluacionInicial.Add(evaluacion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Evaluación registrada", evaluacion.ApgarMin1, evaluacion.ApgarMin5 });
        }
    }
}

// =============================================
// SIGNO VITAL NEONATAL
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignoVitalNeonatalController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public SignoVitalNeonatalController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetSignosVitales()
        {
            var data = await (
                from sv in _context.SignoVitalNeonatal
                join rn in _context.RecienNacido on sv.RecienNacidoId equals rn.Id
                join ts in _context.TipoSignoVitalNeonatal on sv.TipoSignoId equals ts.Id
                orderby sv.FechaHora descending
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    sv.CIEmpleado,
                    Tipo_Signo = ts.Nombre,
                    ts.Unidad,
                    sv.Valor,
                    sv.FechaHora,
                    Fuera_de_Rango = sv.Valor < ts.ValorMin || sv.Valor > ts.ValorMax
                }
            ).ToListAsync();
            return Ok(data);
        }

        [HttpGet("por-recien-nacido/{id}")]
        public async Task<IActionResult> GetPorRecienNacido(int id)
        {
            var data = await (
                from sv in _context.SignoVitalNeonatal
                join ts in _context.TipoSignoVitalNeonatal on sv.TipoSignoId equals ts.Id
                where sv.RecienNacidoId == id
                orderby sv.FechaHora
                select new
                {
                    sv.FechaHora,
                    sv.CIEmpleado,
                    Tipo_Signo = ts.Nombre,
                    ts.Unidad,
                    sv.Valor,
                    ts.ValorMin,
                    ts.ValorMax,
                    Fuera_de_Rango = sv.Valor < ts.ValorMin || sv.Valor > ts.ValorMax
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No hay signos vitales para recién nacido ID: {id}" });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostSignoVital([FromBody] SignoVitalNeonatal signo)
        {
            var rn = await _context.RecienNacido.FindAsync(signo.RecienNacidoId);
            if (rn == null)
                return NotFound(new { Mensaje = "Recién nacido no encontrado" });

            var tipoSigno = await _context.TipoSignoVitalNeonatal.FindAsync(signo.TipoSignoId);
            if (tipoSigno == null)
                return NotFound(new { Mensaje = "Tipo de signo vital no encontrado" });

            _context.SignoVitalNeonatal.Add(signo);
            await _context.SaveChangesAsync();

            // Alerta automática si está fuera de rango
            if (signo.Valor < tipoSigno.ValorMin || signo.Valor > tipoSigno.ValorMax)
            {
                var alerta = new AlertaNeonatal
                {
                    RecienNacidoId = signo.RecienNacidoId,
                    TipoSignoId = signo.TipoSignoId,
                    TipoAlerta = signo.Valor < tipoSigno.ValorMin * 0.9m || signo.Valor > tipoSigno.ValorMax * 1.1m
                                     ? "Critica" : "Moderada",
                    Descripcion = $"{tipoSigno.Nombre} fuera de rango: {signo.Valor} {tipoSigno.Unidad} (rango normal: {tipoSigno.ValorMin}-{tipoSigno.ValorMax})",
                    FechaGenerada = DateTime.UtcNow
                };
                _context.AlertaNeonatal.Add(alerta);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                Mensaje = "Signo vital registrado",
                signo.Id,
                Tipo = tipoSigno.Nombre,
                signo.Valor,
                Fuera_de_Rango = signo.Valor < tipoSigno.ValorMin || signo.Valor > tipoSigno.ValorMax
            });
        }
    }
}

// =============================================
// TRATAMIENTO NEONATAL
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientoNeonatalController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public TratamientoNeonatalController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTratamientos()
        {
            var data = await (
                from t in _context.TratamientoNeonatal
                join rn in _context.RecienNacido on t.RecienNacidoId equals rn.Id
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    t.CIEmpleado,
                    t.CodigoMedicamento,
                    t.TipoTratamiento,
                    t.Descripcion,
                    t.FechaInicio,
                    t.FechaFin,
                    Estado_Tratamiento = t.FechaFin == null ? "En curso" : "Finalizado"
                }
            ).ToListAsync();
            return Ok(data);
        }

        [HttpGet("por-recien-nacido/{id}")]
        public async Task<IActionResult> GetPorRecienNacido(int id)
        {
            var data = await (
                from t in _context.TratamientoNeonatal
                where t.RecienNacidoId == id
                select new
                {
                    t.CIEmpleado,
                    t.CodigoMedicamento,
                    t.TipoTratamiento,
                    t.Descripcion,
                    t.FechaInicio,
                    t.FechaFin,
                    Estado = t.FechaFin == null ? "En curso" : "Finalizado"
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No hay tratamientos para recién nacido ID: {id}" });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostTratamiento([FromBody] TratamientoNeonatal tratamiento)
        {
            var rn = await _context.RecienNacido.FindAsync(tratamiento.RecienNacidoId);
            if (rn == null)
                return NotFound(new { Mensaje = "Recién nacido no encontrado" });

            _context.TratamientoNeonatal.Add(tratamiento);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Tratamiento registrado", tratamiento.Id, tratamiento.TipoTratamiento, tratamiento.FechaInicio });
        }
    }
}

// =============================================
// COMPLICACION MATERNA
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplicacionMaternaController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public ComplicacionMaternaController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetComplicaciones()
        {
            var data = await (
                from cm in _context.ComplicacionMaterna
                join p in _context.Parto on cm.PartoId equals p.Id
                select new
                {
                    CIPaciente = p.CIPaciente,
                    Fecha_Parto = p.FechaHora,
                    Tipo_Parto = p.TipoParto,
                    cm.TipoComplicacion,
                    cm.Descripcion,
                    cm.TratamientoAplicado,
                    cm.FechaRegistro
                }
            ).OrderByDescending(x => x.FechaRegistro).ToListAsync();
            return Ok(data);
        }

        [HttpGet("por-parto/{partoId}")]
        public async Task<IActionResult> GetPorParto(int partoId)
        {
            var data = await (
                from cm in _context.ComplicacionMaterna
                where cm.PartoId == partoId
                select new
                {
                    cm.TipoComplicacion,
                    cm.Descripcion,
                    cm.TratamientoAplicado,
                    cm.FechaRegistro
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No hay complicaciones para parto ID: {partoId}" });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostComplicacion([FromBody] ComplicacionMaterna complicacion)
        {
            var parto = await _context.Parto.FindAsync(complicacion.PartoId);
            if (parto == null)
                return NotFound(new { Mensaje = "Parto no encontrado" });

            _context.ComplicacionMaterna.Add(complicacion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Complicación materna registrada", complicacion.Id, complicacion.TipoComplicacion });
        }
    }
}

// =============================================
// COMPLICACION NEONATAL
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplicacionNeonatalController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public ComplicacionNeonatalController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetComplicaciones()
        {
            var data = await (
                from cn in _context.ComplicacionNeonatal
                join rn in _context.RecienNacido on cn.RecienNacidoId equals rn.Id
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    cn.CIEmpleado,
                    cn.TipoComplicacion,
                    cn.Descripcion,
                    cn.TratamientoAplicado,
                    cn.FechaRegistro
                }
            ).OrderByDescending(x => x.FechaRegistro).ToListAsync();
            return Ok(data);
        }

        [HttpGet("por-recien-nacido/{id}")]
        public async Task<IActionResult> GetPorRecienNacido(int id)
        {
            var data = await (
                from cn in _context.ComplicacionNeonatal
                where cn.RecienNacidoId == id
                select new
                {
                    cn.TipoComplicacion,
                    cn.Descripcion,
                    cn.TratamientoAplicado,
                    cn.FechaRegistro
                }
            ).ToListAsync();

            if (!data.Any())
                return NotFound(new { Mensaje = $"No hay complicaciones para recién nacido ID: {id}" });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostComplicacion([FromBody] ComplicacionNeonatal complicacion)
        {
            var rn = await _context.RecienNacido.FindAsync(complicacion.RecienNacidoId);
            if (rn == null)
                return NotFound(new { Mensaje = "Recién nacido no encontrado" });

            _context.ComplicacionNeonatal.Add(complicacion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Complicación neonatal registrada", complicacion.Id, complicacion.TipoComplicacion });
        }
    }
}

// =============================================
// ALERTA NEONATAL
// =============================================
namespace Maternidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaNeonatalController : ControllerBase
    {
        private readonly MaternidadContext _context;
        public AlertaNeonatalController(MaternidadContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAlertas()
        {
            var data = await (
                from a in _context.AlertaNeonatal
                join rn in _context.RecienNacido on a.RecienNacidoId equals rn.Id
                join ts in _context.TipoSignoVitalNeonatal on a.TipoSignoId equals ts.Id
                orderby a.FechaGenerada descending
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    Tipo_Signo = ts.Nombre,
                    a.TipoAlerta,
                    a.Descripcion,
                    a.FechaGenerada
                }
            ).ToListAsync();
            return Ok(data);
        }

        [HttpGet("criticas")]
        public async Task<IActionResult> GetCriticas()
        {
            var data = await (
                from a in _context.AlertaNeonatal
                join rn in _context.RecienNacido on a.RecienNacidoId equals rn.Id
                join ts in _context.TipoSignoVitalNeonatal on a.TipoSignoId equals ts.Id
                where a.TipoAlerta == "Critica"
                orderby a.FechaGenerada descending
                select new
                {
                    RecienNacido = rn.Nombre,
                    rn.CIPaciente,
                    Tipo_Signo = ts.Nombre,
                    a.Descripcion,
                    a.FechaGenerada
                }
            ).ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostAlerta([FromBody] AlertaNeonatal alerta)
        {
            var rn = await _context.RecienNacido.FindAsync(alerta.RecienNacidoId);
            if (rn == null)
                return NotFound(new { Mensaje = "Recién nacido no encontrado" });

            _context.AlertaNeonatal.Add(alerta);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Alerta registrada", alerta.Id, alerta.TipoAlerta, alerta.FechaGenerada });
        }
    }
}