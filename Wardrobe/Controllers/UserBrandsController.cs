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
    public class UserBrandsController : ControllerBase
    {
        private IUnitOfWork _uow;

        public UserBrandsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/UserBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBrand>>> GetUserBrands()
        {
            var userBrands = await _uow.UserBrandRepository.GetAllAsync();

            return userBrands.ToList();
        }

        // GET: api/UserBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBrand>> GetUserBrand(int id)
        {
            var userBrand = await _uow.UserBrandRepository.GetByIDAsync(id);

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

            _uow.UserBrandRepository.Update(userBrand);

            try
            {
                await _uow.SaveAsync();
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
            _uow.UserBrandRepository.Insert(userBrand);
            await _uow.SaveAsync();

            return CreatedAtAction("GetUserBrand", new { id = userBrand.Id }, userBrand);
        }

        // DELETE: api/UserBrands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBrand(int id)
        {
            var userBrand = await _uow.UserBrandRepository.GetByIDAsync(id);
            if (userBrand == null)
            {
                return NotFound();
            }

            _uow.UserBrandRepository.Delete(id);
            await _uow.SaveAsync();

            return NoContent();
        }

        private bool UserBrandExists(int id)
        {
            return _uow.UserBrandRepository.Get(e => e.Id == id).Any();
        }
    }
}
