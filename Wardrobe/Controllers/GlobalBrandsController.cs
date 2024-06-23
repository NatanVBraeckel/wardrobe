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
    public class GlobalBrandsController : ControllerBase
    {
        private readonly WardrobeContext _context;

        public GlobalBrandsController(WardrobeContext context)
        {
            _context = context;
        }

        // GET: api/GlobalBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalBrand>>> GetGlobalBrands()
        {
            return await _context.GlobalBrands.ToListAsync();
        }

        // GET: api/GlobalBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlobalBrand>> GetGlobalBrand(int id)
        {
            var globalBrand = await _context.GlobalBrands.FindAsync(id);

            if (globalBrand == null)
            {
                return NotFound();
            }

            return globalBrand;
        }

        // PUT: api/GlobalBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGlobalBrand(int id, GlobalBrand globalBrand)
        {
            if (id != globalBrand.Id)
            {
                return BadRequest();
            }

            _context.Entry(globalBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalBrandExists(id))
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

        // POST: api/GlobalBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GlobalBrand>> PostGlobalBrand(GlobalBrand globalBrand)
        {
            _context.GlobalBrands.Add(globalBrand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGlobalBrand", new { id = globalBrand.Id }, globalBrand);
        }

        // DELETE: api/GlobalBrands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlobalBrand(int id)
        {
            var globalBrand = await _context.GlobalBrands.FindAsync(id);
            if (globalBrand == null)
            {
                return NotFound();
            }

            _context.GlobalBrands.Remove(globalBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GlobalBrandExists(int id)
        {
            return _context.GlobalBrands.Any(e => e.Id == id);
        }
    }
}
