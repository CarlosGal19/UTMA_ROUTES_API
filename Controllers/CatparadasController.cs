using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CatparadasController : ControllerBase
    {
        private readonly UTMARutasDbContext _context;

        public CatparadasController(UTMARutasDbContext context)
        {
            _context = context;
        }

        // POST : Catparadas/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ReqCatParada catparada)
        {
            if (catparada == null)
            {
                return BadRequest("Invalid parada data.");
            }
            var newParada = new Catparada
            {
                ECodRuta = catparada.eCodRuta,
                TNombre = catparada.tNombre,
                DLatitud = catparada.dLatitud,
                DLongitud = catparada.dLongitud
            };
            _context.Catparadas.Add(newParada);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Parada created successfully", paradaId = newParada.ECodParada });
        }

        // GET: Catparadas
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<ResCatParada>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAll()
        {
            var paradas = await _context.Catparadas
                .Include(p => p.ECodRutaNavigation)
                .Select(p => new ResCatParada
                {
                    eCodParada = p.ECodParada,
                    tNombre = p.TNombre,
                    dLatitud = p.DLatitud,
                    dLongitud = p.DLongitud,
                    tRutaNombre = p.ECodRutaNavigation.TNombre
                })
                .ToListAsync();
            return Ok(paradas);
        }

        // GET: Catparadas/:id
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResCatParada), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var parada = await _context.Catparadas
                .Include(p => p.ECodRutaNavigation)
                .Where(p => p.ECodParada == id)
                .Select(p => new ResCatParada
                {
                    eCodParada = p.ECodParada,
                    tNombre = p.TNombre,
                    dLatitud = p.DLatitud,
                    dLongitud = p.DLongitud,
                    tRutaNombre = p.ECodRutaNavigation.TNombre
                })
                .FirstOrDefaultAsync();
            if (parada == null)
            {
                return NotFound("Parada not found.");
            }
            return Ok(parada);
        }

        // DELETE: Catparadas/Delete/:id
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parada = await _context.Catparadas.FindAsync(id);
            if (parada == null)
            {
                return NotFound("Parada not found.");
            }
            _context.Catparadas.Remove(parada);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Parada deleted successfully" });
        }

        // PATCH: Catparadas/:id
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReqCatParada catparada)
        {
            var existingParada = await _context.Catparadas.FindAsync(id);
            if (existingParada == null)
            {
                return NotFound("Parada not found.");
            }
            existingParada.TNombre = catparada.tNombre;
            existingParada.DLatitud = catparada.dLatitud;
            existingParada.DLongitud = catparada.dLongitud;
            existingParada.ECodRuta = catparada.eCodRuta;
            _context.Catparadas.Update(existingParada);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Parada updated successfully" });
        }
    }
}
