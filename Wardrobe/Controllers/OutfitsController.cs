using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wardrobe.DAL;
using Wardrobe.DAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wardrobe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitsController : ControllerBase
    {
        private IUnitOfWork _uow;

        public OutfitsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Outfits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Outfit>>> GetOutfits()
        {
            var outfits = await _uow.OutfitRepository.GetAsync(
                includes:
                [
                    o => o.Garments,
                    o => o.User,
                ]
            );

            return outfits.ToList();
        }

        // GET: api/Outfits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Outfit>> GetOutfit(int id)
        {
            var outfit = await _uow.OutfitRepository.GetByIDAsync(id);

            if (outfit == null)
            {
                return NotFound();
            }

            return outfit;
        }

        // PUT: api/Outfits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutfit(int id, Outfit outfit)
        {
            if (id != outfit.Id)
            {
                return BadRequest();
            }

            _uow.OutfitRepository.Update(outfit);

            try
            {
                await _uow.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutfitExists(id))
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

        // POST: api/Outfits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Outfit>> PostOutfit(Outfit outfit)
        {
            _uow.OutfitRepository.Insert(outfit);
            await _uow.SaveAsync();

            return CreatedAtAction("getOutfit", new { id = outfit.Id }, outfit);
        }

        // DELETE: api/Outfits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutfit(int id)
        {
            var outfit = await _uow.OutfitRepository.GetByIDAsync(id);
            if (outfit == null)
            {
                return NotFound();
            }

            _uow.OutfitRepository.Delete(id);
            await _uow.SaveAsync();

            return NoContent();
        }

        private bool OutfitExists(int id)
        {
            return _uow.OutfitRepository.Get(e => e.Id == id).Any();
        }
    }
}
