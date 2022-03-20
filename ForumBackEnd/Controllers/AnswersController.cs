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
using ForumBackEnd.Services;
using ForumBackEnd.DTO;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private AnswerServices answerServices;

        public AnswersController(AnswerServices answerServices)
        {
            this.answerServices = answerServices;
        }

        // GET: api/Answers
        [HttpGet]
        public IEnumerable<Answer> GetAnswers()
        {
            return answerServices.FindAllAnswers();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public ActionResult<Answer> GetAnswer(int id)
        {
            var answer = answerServices.FindAnswerById(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAnswer(int id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest(new MessageDTO { Message = "Las id's no coinciden"});
            }

            if (!answerServices.ExistByid(answer.Id))
            {
                return BadRequest(new MessageDTO { Message = "No existe el mensaje que desea modificar" });
            }
            return NoContent();
        }

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Answer> PostAnswer(AnswerDTO answerDTO)
        {
            Answer answer = new Answer()
            {
                Title = answerDTO.Title,
                Description = answerDTO.Description,
                Code = answerDTO.Code,
                UserId = answerDTO.UserId,
                QuestionId = answerDTO.QuestionId
            };

            if (!answerServices.CreateAnswer(answer))
            {
                return BadRequest(new MessageDTO { Message = "Error la question o usuario introducidos no existen"});
            }

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            Answer answer = answerServices.FindAnswerById(id);
            if (answer == null)
            {
                return NotFound();
            }
            answerServices.DeteleAnswer(answer);
            return NoContent();
        }

        private bool AnswerExists(int id)
        {
            return answerServices.ExistByid(id);
        }
    }
}
