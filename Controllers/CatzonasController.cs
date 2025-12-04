using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTMA_ROUTES_API.DTO.requests;
using UTMA_ROUTES_API.DTO.responses;
using UTMA_ROUTES_API.Models;

namespace UTMA_ROUTES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatzonasController : ControllerBase
    {
        private readonly UTMARutasDbContext _context;

        public CatzonasController(UTMARutasDbContext context)
        {
            _context = context;
        }

        // POST: Catzonas/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReqCatZona catzona)
        {
            var newCatzona = new Catzona
            {
                TNombre = catzona.tNombre,
                TDescripcion = catzona.tDescripcion
            };
            _context.Add(newCatzona);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Catzona created successfully", id = newCatzona.ECodZona });
        }

        // GET : Catzonas
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<ResZonas>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var catzonas = await _context.Catzonas
                .Select(z => new ResZonas
                {
                    eCodZona = z.ECodZona,
                    tNombre = z.TNombre,
                    tDescripcion = z.TDescripcion
                })
                .ToListAsync();
            return Ok(catzonas);
        }

        // GET : Catzonas/:id
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResZonas), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var catzona = await _context.Catzonas
                .Where(z => z.ECodZona == id)
                .Select(z => new ResZonas
                {
                    eCodZona = z.ECodZona,
                    tNombre = z.TNombre,
                    tDescripcion = z.TDescripcion
                })
                .FirstOrDefaultAsync();
            if (catzona == null)
            {
                return NotFound(new { message = "Catzona not found" });
            }
            return Ok(catzona);
        }

        // DELETE : Catzonas/:id
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catzona = await _context.Catzonas.FindAsync(id);
            if (catzona == null)
            {
                return NotFound(new { message = "Catzona not found" });
            }
            _context.Catzonas.Remove(catzona);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Catzona deleted successfully" });
        }

        // PATCH : Catzonas/:id
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReqCatZona catzona)
        {
            var existingCatzona = await _context.Catzonas.FindAsync(id);
            if (existingCatzona == null)
            {
                return NotFound(new { message = "Catzona not found" });
            }
            existingCatzona.TNombre = catzona.tNombre;
            existingCatzona.TDescripcion = catzona.tDescripcion;
            _context.Catzonas.Update(existingCatzona);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Catzona updated successfully" });
        }
    }
}
