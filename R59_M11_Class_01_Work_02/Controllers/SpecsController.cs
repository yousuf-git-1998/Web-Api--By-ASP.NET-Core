using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R59_M11_Class_01_Work_02.Models_;

namespace R59_M11_Class_01_Work_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecsController : ControllerBase
    {
        private readonly DeviceDbContext _context;

        public SpecsController(DeviceDbContext context)
        {
            _context = context;
        }

        // GET: api/Specs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spec>>> GetSpecs()
        {
            return await _context.Specs.ToListAsync();
        }

        // GET: api/Specs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spec>> GetSpec(int id)
        {
            var spec = await _context.Specs.FindAsync(id);

            if (spec == null)
            {
                return NotFound();
            }

            return spec;
        }

        // PUT: api/Specs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpec(int id, Spec spec)
        {
            if (id != spec.SpecId)
            {
                return BadRequest();
            }

            _context.Entry(spec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecExists(id))
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

        // POST: api/Specs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spec>> PostSpec(Spec spec)
        {
            _context.Specs.Add(spec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpec", new { id = spec.SpecId }, spec);
        }

        // DELETE: api/Specs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpec(int id)
        {
            var spec = await _context.Specs.FindAsync(id);
            if (spec == null)
            {
                return NotFound();
            }

            _context.Specs.Remove(spec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecExists(int id)
        {
            return _context.Specs.Any(e => e.SpecId == id);
        }
    }
}
