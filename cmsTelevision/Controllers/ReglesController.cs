﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmsTelevision.Models;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace cmsTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReglesController : ControllerBase
    {
        private readonly cmsTelevisionContext _context;
        public static IHostingEnvironment _environment;

        public ReglesController(cmsTelevisionContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Regles
        [HttpGet]
        public IEnumerable<Regle> GetRegle()
        {
            return _context.Regle;
        }

        // GET: api/Regles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regle = await _context.Regle.FindAsync(id);

            if (regle == null)
            {
                return NotFound();
            }

            return Ok(regle);
        }

        // PUT: api/Regles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegle([FromRoute] int id, [FromBody] Regle regle)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != regle.Id)
            {
                return BadRequest();
            }

            _context.Entry(regle).State = EntityState.Modified;

            if (regle.Image.Contains("Upload"))
            {
                System.Diagnostics.Debug.WriteLine(regle.Image);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Base64");
                var imagePath = ConvertImage(regle.Image);
                regle.Image = imagePath;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegleExists(id))
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

        // POST: api/Regles
        [HttpPost]
        public async Task<IActionResult> PostRegle([FromBody] Regle regle)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Regle.Add(regle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegle", new { id = regle.Id }, regle);*/

            var imagePath = ConvertImage(regle.Image);
            regle.Image = imagePath;
            _context.Regle.Add(regle);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetRegle", new { id = regle.Id }, regle);
        }

        // DELETE: api/Regles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regle = await _context.Regle.FindAsync(id);
            if (regle == null)
            {
                return NotFound();
            }

            _context.Regle.Remove(regle);
            await _context.SaveChangesAsync();

            return Ok(regle);
        }

        private bool RegleExists(int id)
        {
            return _context.Regle.Any(e => e.Id == id);
        }

        public string ConvertImage(string image)
        {
            // var base64Data = Regex.Match(image, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            // var type = Regex.Match(image, @"data:image/(?<type>.+?),").Groups["data"].Value;
            var match = Regex.Match(image, @"data:image/(?<type>.+?);base64,(?<data>.+)");
            var base64Data = match.Groups["data"].Value;
            var contentType = match.Groups["type"].Value;
            System.Diagnostics.Debug.WriteLine(contentType);
            var bytes = Convert.FromBase64String(base64Data);
            string folderName = "Upload";
            string webRootPath = _environment.WebRootPath;
            string pathToSave = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fileName = Guid.NewGuid().ToString() + "." + contentType;
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return dbPath;
        }
    }
}