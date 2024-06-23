using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wardrobe.DAL.Data;
using Wardrobe.DAL.Models;

namespace Wardrobe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarmentsController : ControllerBase
    {
        private readonly WardrobeContext _context;

        public GarmentsController(WardrobeContext context)
        {
            _context = context;
        }

        // GET: api/Garments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garment>>> GetGarments()
        {
            return await _context.Garments.ToListAsync();
        }

        // GET: api/Garments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Garment>> GetGarment(int id)
        {
            var garment = await _context.Garments.FindAsync(id);

            if (garment == null)
            {
                return NotFound();
            }

            return garment;
        }

        // PUT: api/Garments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarment(int id, Garment garment)
        {
            if (id != garment.Id)
            {
                return BadRequest();
            }

            _context.Entry(garment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Garments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Garment>> PostGarment(Garment garment)
        {
            _context.Garments.Add(garment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarment", new { id = garment.Id }, garment);
        }

        // DELETE: api/Garments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarment(int id)
        {
            var garment = await _context.Garments.FindAsync(id);
            if (garment == null)
            {
                return NotFound();
            }

            _context.Garments.Remove(garment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GarmentExists(int id)
        {
            return _context.Garments.Any(e => e.Id == id);
        }
    }
}
