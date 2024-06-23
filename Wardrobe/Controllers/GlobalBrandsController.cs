using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wardrobe.DAL;
using Wardrobe.DAL.Data;
using Wardrobe.DAL.Models;

namespace Wardrobe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalBrandsController : ControllerBase
    {
        private IUnitOfWork _uow;

        public GlobalBrandsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/GlobalBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalBrand>>> GetGlobalBrands()
        {
            var globalBrands = await _uow.GlobalBrandRepository.GetAsync(
                includes:
                [
                    b => b.Garments,
                ]
            );

            return globalBrands.ToList();
        }

        // GET: api/GlobalBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlobalBrand>> GetGlobalBrand(int id)
        {
            var globalBrand = await _uow.GlobalBrandRepository.GetByIDAsync(id);

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

            _uow.GlobalBrandRepository.Update(globalBrand);

            try
            {
                await _uow.SaveAsync();
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
            _uow.GlobalBrandRepository.Insert(globalBrand);
            await _uow.SaveAsync();

            return CreatedAtAction("GetGlobalBrand", new { id = globalBrand.Id }, globalBrand);
        }

        // DELETE: api/GlobalBrands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlobalBrand(int id)
        {
            var globalBrand = await _uow.GlobalBrandRepository.GetByIDAsync(id);
            if (globalBrand == null)
            {
                return NotFound();
            }

            _uow.GlobalBrandRepository.Delete(id);
            await _uow.SaveAsync();

            return NoContent();
        }

        private bool GlobalBrandExists(int id)
        {
            return _uow.GlobalBrandRepository.Get(e => e.Id == id).Any();
        }
    }
}
