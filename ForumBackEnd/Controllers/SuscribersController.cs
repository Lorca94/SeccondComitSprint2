#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SuscribersController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public SuscribersController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/Suscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suscriber>>> GetSuscriber()
        {
            return await _context.Suscriber.ToListAsync();
        }

        // GET: api/Suscribers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suscriber>> GetSuscriber(int id)
        {
            var suscriber = await _context.Suscriber.FindAsync(id);

            if (suscriber == null)
            {
                return NotFound();
            }

            return suscriber;
        }

        // PUT: api/Suscribers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuscriber(int id, Suscriber suscriber)
        {
            if (id != suscriber.Id)
            {
                return BadRequest();
            }

            _context.Entry(suscriber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuscriberExists(id))
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

        // POST: api/Suscribers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suscriber>> PostSuscriber(Suscriber suscriber)
        {
            _context.Suscriber.Add(suscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuscriber", new { id = suscriber.Id }, suscriber);
        }

        // DELETE: api/Suscribers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuscriber(int id)
        {
            var suscriber = await _context.Suscriber.FindAsync(id);
            if (suscriber == null)
            {
                return NotFound();
            }

            _context.Suscriber.Remove(suscriber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuscriberExists(int id)
        {
            return _context.Suscriber.Any(e => e.Id == id);
        }
    }
}
