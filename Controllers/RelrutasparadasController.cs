using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTMA_ROUTES_API.DTO.requests;
using UTMA_ROUTES_API.DTO.responses;
using UTMA_ROUTES_API.Models;

namespace UTMA_ROUTES_API.Controllers
{
    [ApiController]
    public class RelrutasparadasController : ControllerBase
    {
        private readonly UTMARutasDbContext _context;

        public RelrutasparadasController(UTMARutasDbContext context)
        {
            _context = context;
        }

        // POST: Relrutasparadas/Create
        [HttpPost("api/Relrutasparadas/Create")]
        public async Task<IActionResult> Create([FromBody] ReqRelRutasParadas relrutasparada)
        {
            var newRelRutaParada = new Relrutasparada
            {
                ECodRuta = relrutasparada.ECodRuta,
                ECodParada = relrutasparada.ECodParada
            };

            _context.Relrutasparadas.Add(newRelRutaParada);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Relación creada exitosamente", id = newRelRutaParada.ECodRutasParadas });
        }

        // GET: Relrutasparadas
        [HttpGet("api/Relrutasparadas")]
        [ProducesResponseType(typeof(List<ResRelRutasParadas>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRelRutasParadas()
        {
            var relRutasParadas = await _context.Relrutasparadas
                .Include(r => r.ECodRutaNavigation)
                .Include(r => r.ECodParadaNavigation)
                .Select(r => new ResRelRutasParadas
                {
                    ECodRutasParadas = r.ECodRutasParadas,
                    TNombreRuta = r.ECodRutaNavigation.TNombre,
                    TNombreParada = r.ECodParadaNavigation.TNombre
                })
                .ToListAsync();
            return Ok(relRutasParadas);
        }

        // GET: Relrutasparadas/:id
        [HttpGet("api/Relrutasparadas/{id}")]
        public async Task<IActionResult> GetRelRutasParada(int id)
        {
            var relRutasParada = await _context.Relrutasparadas
                .Include(r => r.ECodRutaNavigation)
                .Include(r => r.ECodParadaNavigation)
                .Where(r => r.ECodRutasParadas == id)
                .Select(r => new ResRelRutasParadas
                {
                    ECodRutasParadas = r.ECodRutasParadas,
                    TNombreRuta = r.ECodRutaNavigation.TNombre,
                    TNombreParada = r.ECodParadaNavigation.TNombre
                })
                .FirstOrDefaultAsync();
            if (relRutasParada == null)
            {
                return NotFound(new { message = "Relación no encontrada" });
            }
            return Ok(relRutasParada);
        }

        // DELETE: Relrutasparadas/Delete/:id

        [HttpDelete("api/Relrutasparadas/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var relRutasParada = _context.Relrutasparadas.Find(id);
            if (relRutasParada == null)
            {
                return NotFound(new { message = "Relación no encontrada" });
            }
            _context.Relrutasparadas.Remove(relRutasParada);
            _context.SaveChanges();
            return Ok(new { message = "Relación eliminada exitosamente" });
        }

        // PATCH: Relrutasparadas/Update/:id
        [HttpPatch("api/Relrutasparadas/Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReqRelRutasParadas relrutasparada)
        {
            var existingRelRutaParada = await _context.Relrutasparadas.FindAsync(id);
            if (existingRelRutaParada == null)
            {
                return NotFound(new { message = "Relación no encontrada" });
            }
            existingRelRutaParada.ECodRuta = relrutasparada.ECodRuta;
            existingRelRutaParada.ECodParada = relrutasparada.ECodParada;
            _context.Relrutasparadas.Update(existingRelRutaParada);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Relación actualizada exitosamente" });
        }
    }
}
