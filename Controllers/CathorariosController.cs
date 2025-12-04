using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTMA_ROUTES_API.DTO.requests;
using UTMA_ROUTES_API.DTO.responses;
using UTMA_ROUTES_API.Models;

namespace UTMA_ROUTES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CathorariosController : ControllerBase
    {
        private readonly UTMARutasDbContext _context;

        public CathorariosController(UTMARutasDbContext context)
        {
            _context = context;
        }

        // POST : Cathorarios/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReqCatHorario cathorario)
        {
            var newCathorario = new Cathorario
            {
                ECodRuta = cathorario.eCodRuta,
                TmHoraSalida = cathorario.tmHoraSalida,
                TmHoraEntrada = cathorario.tmHoraEntrada
            };
            _context.Add(newCathorario);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Cathorario created successfully", id = newCathorario.ECodHorario });
        }

        // GET: Cathorarios
        [HttpGet]
        [ProducesResponseType(typeof(List<ResCatHorarios>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCathorarios()
        {
            var cathorarios = await _context.Cathorarios
                .Include(c => c.ECodRutaNavigation)
                .Select(c => new ResCatHorarios
                {
                    eCodHorario = c.ECodHorario,
                    tRutaNombre = c.ECodRutaNavigation.TNombre,
                    tmHoraSalida = c.TmHoraSalida,
                    tmHoraEntrada = c.TmHoraEntrada
                })
                .ToListAsync();
            return Ok(cathorarios);
        }

        // GET : Cathorarios/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCathorario(int id)
        {
            var cathorario = await _context.Cathorarios
                .Include(c => c.ECodRutaNavigation)
                .Where(c => c.ECodHorario == id)
                .Select(c => new ResCatHorarios
                {
                    eCodHorario = c.ECodHorario,
                    tRutaNombre = c.ECodRutaNavigation.TNombre,
                    tmHoraSalida = c.TmHoraSalida,
                    tmHoraEntrada = c.TmHoraEntrada
                })
                .FirstOrDefaultAsync();
            if (cathorario == null)
            {
                return NotFound(new { message = "Cathorario not found" });
            }
            return Ok(cathorario);
        }

        // DELETE : Cathorarios/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCathorario(int id)
        {
            var cathorario = await _context.Cathorarios.FindAsync(id);
            if (cathorario == null)
            {
                return NotFound(new { message = "Cathorario not found" });
            }
            _context.Cathorarios.Remove(cathorario);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Cathorario deleted successfully" });
        }

        // PATCH : Cathorarios/:id
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCathorario(int id, [FromBody] ReqCatHorario updatedCathorario)
        {
            var cathorario = await _context.Cathorarios.FindAsync(id);
            if (cathorario == null)
            {
                return NotFound(new { message = "Cathorario not found" });
            }
            cathorario.ECodRuta = updatedCathorario.eCodRuta;
            cathorario.TmHoraSalida = updatedCathorario.tmHoraSalida;
            cathorario.TmHoraEntrada = updatedCathorario.tmHoraEntrada;
            _context.Cathorarios.Update(cathorario);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Cathorario updated successfully" });
        }
    }
}
