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
    [Route("api/[controller]")]
    [ApiController]
    public class SubQuestionsController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public SubQuestionsController(ForumBackEndContext context)
        {
            _context = context;
        }

        // GET: api/SubQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubQuestion>>> GetSubQuestions()
        {
            return await _context.SubQuestions.ToListAsync();
        }

        // GET: api/SubQuestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubQuestion>> GetSubQuestion(int id)
        {
            var subQuestion = await _context.SubQuestions.FindAsync(id);

            if (subQuestion == null)
            {
                return NotFound();
            }

            return subQuestion;
        }

        // PUT: api/SubQuestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubQuestion(int id, SubQuestion subQuestion)
        {
            if (id != subQuestion.UserId)
            {
                return BadRequest();
            }

            _context.Entry(subQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubQuestionExists(id))
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

        // POST: api/SubQuestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubQuestion>> PostSubQuestion(RelDTO subDTO)
        {
            if(!_context.Users.Any( x => x.Id == subDTO.UserId) 
                || !_context.Questions.Any( q => q.Id == subDTO.RelId ))
            {
                return NotFound();
            } else
            {
                SubQuestion subQuestion = new SubQuestion()
                {
                    UserId = subDTO.UserId,
                    QuestionId = subDTO.RelId
                };
                _context.SubQuestions.Add(subQuestion);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (SubQuestionExists(subQuestion.UserId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetSubQuestion", new { id = subQuestion.UserId }, subQuestion);
            }
        }

        // DELETE: api/SubQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubQuestion(int id)
        {
            var subQuestion = await _context.SubQuestions.FindAsync(id);
            if (subQuestion == null)
            {
                return NotFound();
            }

            _context.SubQuestions.Remove(subQuestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubQuestionExists(int id)
        {
            return _context.SubQuestions.Any(e => e.UserId == id);
        }
    }
}
