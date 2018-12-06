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
    public class ExerciseTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExerciseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseTypes
        [HttpGet]
        public IEnumerable<ExerciseType> GetExerciseType()
        {
            return _context.ExerciseType;
        }

        // GET: api/ExerciseTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exerciseType = await _context.ExerciseType.FindAsync(id);

            if (exerciseType == null)
            {
                return NotFound();
            }

            return Ok(exerciseType);
        }

        // PUT: api/ExerciseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseType([FromRoute] int id, [FromBody] ExerciseType exerciseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exerciseType.ID)
            {
                return BadRequest();
            }

            _context.Entry(exerciseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseTypeExists(id))
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

        // POST: api/ExerciseTypes
        [HttpPost]
        public async Task<IActionResult> PostExerciseType([FromBody] ExerciseType exerciseType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ExerciseType.Add(exerciseType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseType", new { id = exerciseType.ID }, exerciseType);
        }

        // DELETE: api/ExerciseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exerciseType = await _context.ExerciseType.FindAsync(id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            _context.ExerciseType.Remove(exerciseType);
            await _context.SaveChangesAsync();

            return Ok(exerciseType);
        }

        private bool ExerciseTypeExists(int id)
        {
            return _context.ExerciseType.Any(e => e.ID == id);
        }
    }
}