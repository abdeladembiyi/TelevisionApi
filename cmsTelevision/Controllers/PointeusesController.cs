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
    public class PointeusesController : ControllerBase
    {
        private readonly cmsTelevisionContext _context;

        public PointeusesController(cmsTelevisionContext context)
        {
            _context = context;
        }

        // GET: api/Pointeuses
        [HttpGet]
        public IEnumerable<Pointeuse> GetPointeuse()
        {
            return _context.Pointeuse;
        }
        [HttpGet("{etat}")]
        public IEnumerable<bool?> GetPointeuseEtat()
        {
            return _context.Pointeuse.Select(e=>e.Etat);
        }
        // GET: api/Pointeuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPointeuse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pointeuse = await _context.Pointeuse.FindAsync(id);

            if (pointeuse == null)
            {
                return NotFound();
            }

            return Ok(pointeuse);
        }
        [HttpGet("desactiver/{id}")]
        public async Task<ActionResult> DesactiverPointeuse([FromRoute] int id)
        {
            Pointeuse pointeuse = await _context.Pointeuse.FirstOrDefaultAsync(x => x.Id == id);
            if (pointeuse != null)
            {
                pointeuse.Etat = false;
                _context.SaveChanges();
            }
            return Ok();
        }
        [HttpGet("activer/{id}")]
        public async Task<ActionResult> activerPointeuse([FromRoute] int id)
        {
            Pointeuse pointeuse = await _context.Pointeuse.FirstOrDefaultAsync(x => x.Id == id);
            if (pointeuse != null)
            {  
                pointeuse.Etat = true;
                _context.SaveChanges();
            }
            return Ok();
        }
        // PUT: api/Pointeuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointeuse([FromRoute] int id, [FromBody] Pointeuse pointeuse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointeuse.Id)
            {
                return BadRequest();
            }

            _context.Entry(pointeuse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointeuseExists(id))
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

        // POST: api/Pointeuses
        [HttpPost]
        public async Task<IActionResult> PostPointeuse([FromBody] Pointeuse pointeuse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pointeuse.Add(pointeuse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPointeuse", new { id = pointeuse.Id }, pointeuse);
        }

        // DELETE: api/Pointeuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointeuse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pointeuse = await _context.Pointeuse.FindAsync(id);
            if (pointeuse == null)
            {
                return NotFound();
            }

            _context.Pointeuse.Remove(pointeuse);
            await _context.SaveChangesAsync();

            return Ok(pointeuse);
        }

        private bool PointeuseExists(int id)
        {
            return _context.Pointeuse.Any(e => e.Id == id);
        }
    }
}