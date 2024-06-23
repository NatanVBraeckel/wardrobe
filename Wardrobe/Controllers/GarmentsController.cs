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
    public class GarmentsController : ControllerBase
    {
        private IUnitOfWork _uow;

        public GarmentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Garments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garment>>> GetGarments()
        {
            var garments = await _uow.GarmentRepository.GetAsync(
                includes:
                [
                    g => g.GlobalBrand,
                    g => g.UserBrand,
                    g => g.User,
                    g => g.GarmentType,
                ]
            );

            return garments.ToList();
        }

        // GET: api/Garments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Garment>> GetGarment(int id)
        {
            var garment = await _uow.GarmentRepository.GetByIDAsync(id);

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

            _uow.GarmentRepository.Update(garment);

            try
            {
                await _uow.SaveAsync();
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
            _uow.GarmentRepository.Insert(garment);
            await _uow.SaveAsync();

            return CreatedAtAction("GetGarment", new { id = garment.Id }, garment);
        }

        // DELETE: api/Garments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarment(int id)
        {
            var garment = await _uow.GarmentRepository.GetByIDAsync(id);
            if (garment == null)
            {
                return NotFound();
            }

            _uow.GarmentRepository.Delete(id);
            await _uow.SaveAsync();

            return NoContent();
        }

        private bool GarmentExists(int id)
        {
            return _uow.GarmentRepository.Get(e => e.Id == id).Any();
        }
    }
}
