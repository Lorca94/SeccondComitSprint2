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
    [Route("/forum/courses")]
    [ApiController]
    public class CourseUsersController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public CourseUsersController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/CourseUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseUser>>> GetCourseRelations()
        {
            return await _context.CourseRelations.ToListAsync();
        }

        // GET: api/CourseUsers/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CourseUser>>> GetUserCourses(int userId)
        {
            var actualCourses = await _context.CourseRelations.Where(c => c.UserId == userId).ToListAsync();
            return actualCourses;
        }

        // PUT: api/CourseUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseUser(int id, CourseUser courseUser)
        {
            if (id != courseUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(courseUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseUserExists(id))
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

        // POST: api/CourseUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseUser>> PostCourseUser(RelDTO dto)
        {
            if(!_context.Courses.Any(x => x.Id == dto.RelId))
            {
                return NotFound();
            }
            CourseUser courseUser = new CourseUser()
            {
                UserId = dto.UserId,
                CourseId = dto.RelId
            };

            _context.CourseRelations.Add(courseUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseUserExists(courseUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(courseUser);
        }

        // DELETE: api/CourseUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseUser(int id)
        {
            var courseUser = await _context.CourseRelations.FindAsync(id);
            if (courseUser == null)
            {
                return NotFound();
            }

            _context.CourseRelations.Remove(courseUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseUserExists(int id)
        {
            return _context.CourseRelations.Any(e => e.UserId == id);
        }
    }
}
