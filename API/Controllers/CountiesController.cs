using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmarkLiveMap.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountiesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Counties
        [HttpGet]
        public IEnumerable<County> GetCounty()
        {
            return _context.County;
        }

        // GET: api/Counties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCounty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var county = await _context.County.FindAsync(id);

            if (county == null)
            {
                return NotFound();
            }

            return Ok(county);
        }

        // PUT: api/Counties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounty([FromRoute] int id, [FromBody] County county)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != county.ID)
            {
                return BadRequest();
            }

            _context.Entry(county).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyExists(id))
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

        // POST: api/Counties
        [HttpPost]
        public async Task<IActionResult> PostCounty([FromBody] County county)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.County.Add(county);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.ID }, county);
        }

        // DELETE: api/Counties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var county = await _context.County.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _context.County.Remove(county);
            await _context.SaveChangesAsync();

            return Ok(county);
        }

        private bool CountyExists(int id)
        {
            return _context.County.Any(e => e.ID == id);
        }
    }
}