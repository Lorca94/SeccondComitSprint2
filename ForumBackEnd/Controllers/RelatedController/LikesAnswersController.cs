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
using ForumBackEnd.DTO;

namespace ForumBackEnd.Controllers.RelatedController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesAnswersController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public LikesAnswersController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/LikesAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikesAnswer>>> GetLikesAnswers()
        {
            return await _context.LikesAnswers.ToListAsync();
        }

        // GET: api/LikesAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikesAnswer>> GetLikesAnswer(int id)
        {
            var likesAnswer = await _context.LikesAnswers.FindAsync(id);

            if (likesAnswer == null)
            {
                return NotFound();
            }

            return likesAnswer;
        }

        // PUT: api/LikesAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikesAnswer(int id, LikesAnswer likesAnswer)
        {
            if (id != likesAnswer.UserId)
            {
                return BadRequest();
            }

            _context.Entry(likesAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikesAnswerExists(id))
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

        // POST: api/LikesAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikesAnswer>> PostLikesAnswer(LikeDTO dto)
        {
            if(!_context.Users.Any( x => x.Id == dto.UserId)
                || !_context.Answers.Any( x => x.Id == dto.RelId))
            {
                return BadRequest();
            } else
            {
                LikesAnswer likesAnswer = new LikesAnswer()
                {
                    isLike = dto.IsLike,
                    UserId = dto.UserId,
                    AnswerId = dto.RelId
                };
                _context.LikesAnswers.Add(likesAnswer);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (LikesAnswerExists(likesAnswer.UserId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetLikesAnswer", new { id = likesAnswer.UserId }, likesAnswer);
            }
        }

        // DELETE: api/LikesAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikesAnswer(int id)
        {
            var likesAnswer = await _context.LikesAnswers.FindAsync(id);
            if (likesAnswer == null)
            {
                return NotFound();
            }

            _context.LikesAnswers.Remove(likesAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikesAnswerExists(int id)
        {
            return _context.LikesAnswers.Any(e => e.UserId == id);
        }
    }
}
