using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Models;

namespace DeliveryService.Controllers
{
    /// <summary>
    /// CURD <see cref="TypeEquipment"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TypeEquipmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TypeEquipmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TypeEquipments
        [HttpGet]
        public IEnumerable<TypeEquipment> GetTypeEquipments()
        {
            return _context.TypeEquipments;
        }

        // GET: api/TypeEquipments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeEquipment([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeEquipment = await _context.TypeEquipments.FindAsync(id);

            if (typeEquipment == null)
            {
                return NotFound();
            }

            return Ok(typeEquipment);
        }

        // PUT: api/TypeEquipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeEquipment([FromRoute] long id, [FromBody] TypeEquipment typeEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeEquipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeEquipmentExists(id))
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

        // POST: api/TypeEquipments
        [HttpPost]
        public async Task<IActionResult> PostTypeEquipment([FromBody] TypeEquipment typeEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TypeEquipments.Add(typeEquipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeEquipment", new {id = typeEquipment.Id}, typeEquipment);
        }

        // DELETE: api/TypeEquipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeEquipment([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeEquipment = await _context.TypeEquipments.FindAsync(id);
            if (typeEquipment == null)
            {
                return NotFound();
            }

            _context.TypeEquipments.Remove(typeEquipment);
            await _context.SaveChangesAsync();

            return Ok(typeEquipment);
        }

        private bool TypeEquipmentExists(long id)
        {
            return _context.TypeEquipments.Any(e => e.Id == id);
        }
    }
}