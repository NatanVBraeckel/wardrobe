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
    public class GarmentTypesController : ControllerBase
    {
        private readonly WardrobeContext _context;

        public GarmentTypesController(WardrobeContext context)
        {
            _context = context;
        }

        // GET: api/GarmentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarmentType>>> GetGarmentTypes()
        {
            return await _context.GarmentTypes.ToListAsync();
        }

        // GET: api/GarmentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GarmentType>> GetGarmentType(int id)
        {
            var garmentType = await _context.GarmentTypes.FindAsync(id);

            if (garmentType == null)
            {
                return NotFound();
            }

            return garmentType;
        }

        // PUT: api/GarmentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarmentType(int id, GarmentType garmentType)
        {
            if (id != garmentType.Id)
            {
                return BadRequest();
            }

            _context.Entry(garmentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarmentTypeExists(id))
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

        // POST: api/GarmentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GarmentType>> PostGarmentType(GarmentType garmentType)
        {
            _context.GarmentTypes.Add(garmentType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarmentType", new { id = garmentType.Id }, garmentType);
        }

        // DELETE: api/GarmentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarmentType(int id)
        {
            var garmentType = await _context.GarmentTypes.FindAsync(id);
            if (garmentType == null)
            {
                return NotFound();
            }

            _context.GarmentTypes.Remove(garmentType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GarmentTypeExists(int id)
        {
            return _context.GarmentTypes.Any(e => e.Id == id);
        }
    }
}
