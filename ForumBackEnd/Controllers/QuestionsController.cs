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
    public class QuestionsController : ControllerBase
    {
        private QuestionServices questionServices;

        public QuestionsController(QuestionServices questionServices)
        {
            this.questionServices = questionServices;
        }

        // GET: api/Questions
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            return questionServices.FindAllQuestions();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public ActionResult<Question> GetQuestion(int id)
        {
            var question = questionServices.FindQuestionById(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest(new MessageDTO { Message = "Las id's no coinciden" });
            }

            if (!questionServices.ExistsById(id))
            {
                return BadRequest(new MessageDTO { Message = "La pregunta no existe" });
            }

            questionServices.ModifyQuestion(question);
            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Question> PostQuestion(QuestionDTO questionDTO)
        {
            Question question = new Question()
            {
                Title = questionDTO.Title,
                Description = questionDTO.Description,
                Code = questionDTO.Code,
                UserId = questionDTO.UserId,
                ModuleId = questionDTO.ModuleId,
            };
            if (!questionServices.CreateQuestion(question))
            {
                return BadRequest(new MessageDTO { Message = "Error: El módulo o usuario introducidos no existen" });
            }
            return Ok( new MessageDTO {Message = "Pregunta creada con éxisto"});
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            Question question = questionServices.FindQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }

            questionServices.DeleteQuestion(id);

            return NoContent();
        }

        [HttpGet("{id}/setted")]
        public IActionResult SettedQuestion(int id)
        {
            Question question = questionServices.FindQuestionById(id);
            if(id == null)
            {
                return NotFound(new MessageDTO { Message = "No se ha encontrado la question" });
            }
            question.IsSetted = !question.IsSetted;
            questionServices.ModifyQuestion(question);
            return Ok(new MessageDTO { Message ="Modificado con éxito" });
        }

    }
}
