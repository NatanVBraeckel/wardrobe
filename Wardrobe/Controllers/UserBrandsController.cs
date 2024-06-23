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
    public class UserBrandsController : ControllerBase
    {
        private readonly WardrobeContext _context;

        public UserBrandsController(WardrobeContext context)
        {
            _context = context;
        }

        // GET: api/UserBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBrand>>> GetUserBrands()
        {
            return await _context.UserBrands.ToListAsync();
        }

        // GET: api/UserBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBrand>> GetUserBrand(int id)
        {
            var userBrand = await _context.UserBrands.FindAsync(id);

            if (userBrand == null)
            {
                return NotFound();
            }

            return userBrand;
        }

        // PUT: api/UserBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBrand(int id, UserBrand userBrand)
        {
            if (id != userBrand.Id)
            {
                return BadRequest();
            }

            _context.Entry(userBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBrandExists(id))
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

        // POST: api/UserBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserBrand>> PostUserBrand(UserBrand userBrand)
        {
            _context.UserBrands.Add(userBrand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserBrand", new { id = userBrand.Id }, userBrand);
        }

        // DELETE: api/UserBrands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBrand(int id)
        {
            var userBrand = await _context.UserBrands.FindAsync(id);
            if (userBrand == null)
            {
                return NotFound();
            }

            _context.UserBrands.Remove(userBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBrandExists(int id)
        {
            return _context.UserBrands.Any(e => e.Id == id);
        }
    }
}
