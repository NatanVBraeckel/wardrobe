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
    public class GarmentTypesController : ControllerBase
    {
        private IUnitOfWork _uow;

        public GarmentTypesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/GarmentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarmentType>>> GetGarmentTypes()
        {
            var garmentTypes = await _uow.GarmentTypeRepository.GetAsync(
                includes:
                [
                    t => t.Garments,
                ]
            );

            return garmentTypes.ToList();
        }

        // GET: api/GarmentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GarmentType>> GetGarmentType(int id)
        {
            var garmentType = await _uow.GarmentTypeRepository.GetByIDAsync(id);

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

            _uow.GarmentTypeRepository.Update(garmentType);

            try
            {
                await _uow.SaveAsync();
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
            _uow.GarmentTypeRepository.Insert(garmentType);
            await _uow.SaveAsync();

            return CreatedAtAction("GetGarmentType", new { id = garmentType.Id }, garmentType);
        }

        // DELETE: api/GarmentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarmentType(int id)
        {
            var garmentType = await _uow.GarmentTypeRepository.GetByIDAsync(id);
            if (garmentType == null)
            {
                return NotFound();
            }

            _uow.GarmentTypeRepository.Delete(id);
            await _uow.SaveAsync();

            return NoContent();
        }

        private bool GarmentTypeExists(int id)
        {
            return _uow.GarmentTypeRepository.Get(e => e.Id == id).Any();
        }
    }
}
