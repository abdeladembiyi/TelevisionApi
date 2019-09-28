using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmsTelevision.Models;

namespace cmsTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        private readonly cmsTelevisionContext _context;

        public AccidentsController(cmsTelevisionContext context)
        {
            _context = context;
        }

        // GET: api/Accidents
        [HttpGet]
        public IEnumerable<Accident> GetAccident()
        {
            return _context.Accident;
        }

        // GET: api/Accidents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccident([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accident = await _context.Accident.FindAsync(id);

            if (accident == null)
            {
                return NotFound();
            }

            return Ok(accident);
        }

        // PUT: api/Accidents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccident([FromRoute] int id, [FromBody] Accident accident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accident.Id)
            {
                return BadRequest();
            }

            _context.Entry(accident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccidentExists(id))
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

        // POST: api/Accidents
        [HttpPost]
        public async Task<IActionResult> PostAccident([FromBody] Accident accident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Accident.Add(accident);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccident", new { id = accident.Id }, accident);
        }

        // DELETE: api/Accidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccident([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accident = await _context.Accident.FindAsync(id);
            if (accident == null)
            {
                return NotFound();
            }

            _context.Accident.Remove(accident);
            await _context.SaveChangesAsync();

            return Ok(accident);
        }

        private bool AccidentExists(int id)
        {
            return _context.Accident.Any(e => e.Id == id);
        }
    }
}