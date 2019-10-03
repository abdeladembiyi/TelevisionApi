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
    public class RegleCmsController : ControllerBase
    {
        private readonly cmsTelevisionContext _context;

        public RegleCmsController(cmsTelevisionContext context)
        {
            _context = context;
        }

        // GET: api/RegleCms
        [HttpGet]
        public IEnumerable<RegleCms> GetRegleCms()
        {
            return _context.RegleCms;
        }

        // GET: api/RegleCms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegleCms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regleCms = await _context.RegleCms.FindAsync(id);

            if (regleCms == null)
            {
                return NotFound();
            }

            return Ok(regleCms);
        }

        // PUT: api/RegleCms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegleCms([FromRoute] int id, [FromBody] RegleCms regleCms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != regleCms.Id)
            {
                return BadRequest();
            }

            _context.Entry(regleCms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegleCmsExists(id))
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

        // POST: api/RegleCms
        [HttpPost]
        public async Task<IActionResult> PostRegleCms([FromBody] RegleCms regleCms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RegleCms.Add(regleCms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegleCms", new { id = regleCms.Id }, regleCms);
        }

        // DELETE: api/RegleCms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegleCms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regleCms = await _context.RegleCms.FindAsync(id);
            if (regleCms == null)
            {
                return NotFound();
            }

            _context.RegleCms.Remove(regleCms);
            await _context.SaveChangesAsync();

            return Ok(regleCms);
        }

        private bool RegleCmsExists(int id)
        {
            return _context.RegleCms.Any(e => e.Id == id);
        }
    }
}