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
    public class DemarragesController : ControllerBase
    {
        private readonly cmsTelevisionContext _context;

        public DemarragesController(cmsTelevisionContext context)
        {
            _context = context;
        }

        // GET: api/Demarrages
        [HttpGet]
        public IEnumerable<Demarrage> GetDemarrage()
        {
            return _context.Demarrage;
        }
        [HttpGet("demarrage")]
        public DateTime? GetDateDemarrage()
        {
            return _context.Demarrage.Where(a=>a.Type == "demarrage").Select(e=>e.Date).FirstOrDefault();
        }
        [HttpGet("nbrJour")]
        public int GetNombreJours()
        {
            DateTime date = (DateTime)_context.Demarrage.Where(a => a.Type == "demarrage").Select(e => e.Date).FirstOrDefault();
            var nbr = DateTime.Now - date;
            return nbr.Days;
        }
        // GET: api/Demarrages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDemarrage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var demarrage = await _context.Demarrage.FindAsync(id);

            if (demarrage == null)
            {
                return NotFound();
            }

            return Ok(demarrage);
        }

        // PUT: api/Demarrages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemarrage([FromRoute] int id, [FromBody] Demarrage demarrage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demarrage.Id)
            {
                return BadRequest();
            }

            _context.Entry(demarrage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemarrageExists(id))
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

        // POST: api/Demarrages
        [HttpPost]
        public async Task<IActionResult> PostDemarrage([FromBody] Demarrage demarrage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Demarrage.Add(demarrage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemarrage", new { id = demarrage.Id }, demarrage);
        }

        // DELETE: api/Demarrages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemarrage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var demarrage = await _context.Demarrage.FindAsync(id);
            if (demarrage == null)
            {
                return NotFound();
            }

            _context.Demarrage.Remove(demarrage);
            await _context.SaveChangesAsync();

            return Ok(demarrage);
        }

        private bool DemarrageExists(int id)
        {
            return _context.Demarrage.Any(e => e.Id == id);
        }
    }
}