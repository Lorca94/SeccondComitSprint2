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
using ForumBackEnd.DTO.RelationDTO;

namespace ForumBackEnd.Controllers.RelatedController
{
    [Route("forum/modules")]
    [ApiController]
    public class ModuleUsersController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public ModuleUsersController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/ModuleUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleUser>>> GetModuleUser()
        {
            return await _context.ModuleUser.ToListAsync();
        }

        // GET: api/ModuleUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleUser>> GetModuleUser(int id)
        {
            var moduleUser = await _context.ModuleUser.FindAsync(id);

            if (moduleUser == null)
            {
                return NotFound();
            }

            return moduleUser;
        }

        // PUT: api/ModuleUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModuleUser(int id, ModuleUser moduleUser)
        {
            if (id != moduleUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(moduleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleUserExists(id))
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

        // POST: api/ModuleUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleUser>> PostModuleUser(RelDTO dto)
        {
            if(!_context.Users.Any(x => x.Id == dto.UserId)){
                return NotFound();
            }
            if(!_context.Modules.Any(x => x.Id == dto.RelId))
            {
                return NotFound();
            }
            ModuleUser moduleUser = new ModuleUser()
            {
                UserId = dto.UserId,
                ModuleId = dto.RelId
            };

            _context.ModuleUser.Add(moduleUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModuleUserExists(moduleUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetModuleUser", new { id = moduleUser.UserId }, moduleUser);
        }

        // DELETE: api/ModuleUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModuleUser(int id)
        {
            var moduleUser = await _context.ModuleUser.FindAsync(id);
            if (moduleUser == null)
            {
                return NotFound();
            }

            _context.ModuleUser.Remove(moduleUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleUserExists(int id)
        {
            return _context.ModuleUser.Any(e => e.UserId == id);
        }
    }
}
