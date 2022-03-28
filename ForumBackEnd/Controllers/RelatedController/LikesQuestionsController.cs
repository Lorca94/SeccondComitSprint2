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
    public class LikesQuestionsController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public LikesQuestionsController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/LikesQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikesQuestion>>> GetLikesQuestions()
        {
            return await _context.LikesQuestions.ToListAsync();
        }

        // GET: api/LikesQuestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikesQuestion>> GetLikesQuestion(int id)
        {
            var likesQuestion = await _context.LikesQuestions.FindAsync(id);

            if (likesQuestion == null)
            {
                return NotFound();
            }

            return likesQuestion;
        }

        // PUT: api/LikesQuestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikesQuestion(int id, LikesQuestion likesQuestion)
        {
            if (id != likesQuestion.UserId)
            {
                return BadRequest();
            }

            _context.Entry(likesQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikesQuestionExists(id))
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

        // POST: api/LikesQuestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikesQuestion>> PostLikesQuestion(LikeDTO dto)
        {
            if(!_context.Users.Any( x => x.Id == dto.UserId) ||
                !_context.Questions.Any( x => x.Id == dto.RelId))
            {
                return BadRequest();
            } else
            {
                LikesQuestion likesQuestion = new LikesQuestion()
                {
                    isLike = dto.IsLike,
                    UserId = dto.UserId,
                    QuestionId = dto.RelId
                };
                _context.LikesQuestions.Add(likesQuestion);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (LikesQuestionExists(likesQuestion.UserId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetLikesQuestion", new { id = likesQuestion.UserId }, likesQuestion);
            }
        }

        // DELETE: api/LikesQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikesQuestion(int id)
        {
            var likesQuestion = await _context.LikesQuestions.FindAsync(id);
            if (likesQuestion == null)
            {
                return NotFound();
            }

            _context.LikesQuestions.Remove(likesQuestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikesQuestionExists(int id)
        {
            return _context.LikesQuestions.Any(e => e.UserId == id);
        }
    }
}
