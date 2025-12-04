using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTMA_ROUTES_API.DTO.requests;
using UTMA_ROUTES_API.DTO.responses;
using UTMA_ROUTES_API.Models;

namespace UTMA_ROUTES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatrutasController : ControllerBase
    {
        private readonly UTMARutasDbContext _context;

        public CatrutasController(UTMARutasDbContext context)
        {
            _context = context;
        }

        // POST : Catrutas/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReqCatRuta catruta)
        {
            var newCatruta = new Catruta
            {
                TNombre = catruta.tNombre,
                ENumero = catruta.eNumero,
                TDescripcion = catruta.tDescripcion,
                ECodZona = catruta.eCodZona
            };
            _context.Add(newCatruta);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Catruta created successfully", id = newCatruta.ECodRuta });
        }

        // GET: Catrutas
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<ResCatRutas>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var catrutas = await _context.Catrutas
                .Include(r => r.ECodZonaNavigation)
                .Select(r => new ResCatRutas
                {
                    eCodRuta = r.ECodRuta,
                    tNombre = r.TNombre,
                    eNumero = r.ENumero,
                    tDescripcion = r.TDescripcion,
                    tZonaNombre = r.ECodZonaNavigation.TNombre
                })
                .ToListAsync();
            return Ok(catrutas);
        }

        // GET: Catrutas/:id
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResCatRutas), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var catruta = await _context.Catrutas
                .Where(r => r.ECodRuta == id)
                .Include(r => r.ECodZonaNavigation)
                .Select(r => new ResCatRutas
                {
                    eCodRuta = r.ECodRuta,
                    tNombre = r.TNombre,
                    eNumero = r.ENumero,
                    tDescripcion = r.TDescripcion,
                    tZonaNombre = r.ECodZonaNavigation.TNombre
                })
                .FirstOrDefaultAsync();
            if (catruta == null)
            {
                return NotFound(new { message = "Catruta not found" });
            }
            return Ok(catruta);
        }

        // DELETE: Catrutas/:id
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catruta = await _context.Catrutas.FindAsync(id);
            if (catruta == null)
            {
                return NotFound(new { message = "Catruta not found" });
            }
            _context.Catrutas.Remove(catruta);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Catruta deleted successfully" });
        }

        // PATCH: Catrutas/:id
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReqCatRuta catruta)
        {
            var existingCatruta = await _context.Catrutas.FindAsync(id);
            if (existingCatruta == null)
            {
                return NotFound(new { message = "Catruta not found" });
            }
            existingCatruta.TNombre = catruta.tNombre;
            existingCatruta.ENumero = catruta.eNumero;
            existingCatruta.TDescripcion = catruta.tDescripcion;
            existingCatruta.ECodZona = catruta.eCodZona;
            _context.Catrutas.Update(existingCatruta);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Catruta updated successfully" });
        }
    }
}
