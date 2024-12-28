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
    public class CommonSpecsController : ControllerBase
    {
        private readonly DeviceDbContext _context;

        public CommonSpecsController(DeviceDbContext context)
        {
            _context = context;
        }

        // GET: api/CommonSpecs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonSpec>>> GetCommonSpecs()
        {
            return await _context.CommonSpecs.ToListAsync();
        }

        // GET: api/CommonSpecs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommonSpec>> GetCommonSpec(int id)
        {
            var commonSpec = await _context.CommonSpecs.FindAsync(id);

            if (commonSpec == null)
            {
                return NotFound();
            }

            return commonSpec;
        }

        // PUT: api/CommonSpecs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommonSpec(int id, CommonSpec commonSpec)
        {
            if (id != commonSpec.CommonSpecId)
            {
                return BadRequest();
            }

            _context.Entry(commonSpec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommonSpecExists(id))
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

        // POST: api/CommonSpecs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommonSpec>> PostCommonSpec(CommonSpec commonSpec)
        {
            _context.CommonSpecs.Add(commonSpec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommonSpec", new { id = commonSpec.CommonSpecId }, commonSpec);
        }

        // DELETE: api/CommonSpecs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommonSpec(int id)
        {
            var commonSpec = await _context.CommonSpecs.FindAsync(id);
            if (commonSpec == null)
            {
                return NotFound();
            }

            _context.CommonSpecs.Remove(commonSpec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommonSpecExists(int id)
        {
            return _context.CommonSpecs.Any(e => e.CommonSpecId == id);
        }
    }
}
