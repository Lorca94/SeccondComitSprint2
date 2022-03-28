using ForumBackEnd.Data;
using ForumBackEnd.DTO;
using ForumBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ForumBackEnd.DTO.ResponseDTO;

namespace ForumBackEnd.Controllers.FinalControllers
{
    [Route("/foro")]
    [ApiController]
    public class TemasController: ControllerBase
    {
        private readonly ForumBackEndContext _context;
        
        public TemasController(ForumBackEndContext context)
        {
            _context = context;
        }

        #region GET
        /// <summary>
        /// Devuelve todos los modulos a los que está suscrito el usuario
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns> List<Module> </returns>
        [Authorize]
        [HttpGet("temas")]
        public ActionResult<List<TemasDTO>> GetQuestionCourseUsers()
        {
            // Se crea temasUser donde se guardarán los temas del usuario
            List<Question> temasUser = new List<Question>();
            // Se trae la info del user
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            // Se crea el user
            User user = FindUserByToken(identity);
            if (user != null)
            {
                // Se busca los módulos a los que el usuario está suscrito
                var courses = from relation in _context.CourseRelations
                              join course in _context.Courses on relation.CourseId equals course.Id
                              where relation.UserId == user.Id
                              select new Course()
                              {
                                  Id = course.Id,
                                  Description = course.Description,
                                  Modules = course.Modules,
                              };

                // Se itera sobre cada curso
                foreach (var course in courses)
                {
                    // Se itera sobre cada módulo
                    foreach (var module in course.Modules)
                    {
                        // Se obtienen los datos de las questions
                        module.Questions.ForEach(x =>
                        {
                            // Se crea objetos anónimos del tipo TemasDTO para almacenar en temasUser
                            temasUser.Add(new Question()
                            {
                                Title = x.Title,
                                Description = x.Description,
                                Answers = x.Answers
                            }
                            );
                        });
                    }
                }
                // Se ordenan los temas segun si estan fijado por admin o no
                IEnumerable<TemasDTO> orderTemas = from i in temasUser
                                                   orderby i.IsSetted == true
                                                   select new TemasDTO
                                                   {
                                                       Title = i.Title,
                                                       Description = i.Description,
                                                       TotalResponse = i.Answers.Count(),
                                                   };

                // Devuelve el listado de temas
                return orderTemas.ToList();
            }
            // En caso de no encontrar el usuario devuelve un notfound
            return NotFound();
        }

        /// <summary>
        /// Devuelve todas las preguntas de un módulo por su Id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("module={moduleId}/preguntas")]
        public async Task<ActionResult<IEnumerable<TemasDTO>>> GetQuestionById(int moduleId)
        {
            // Se obtiene módulo por id
            Module actualModule = await _context.Modules.FindAsync(moduleId);
            // Si no es null
            if (actualModule != null)
            {
                var result = from x in actualModule.Questions
                             join question in _context.Questions on x.Id equals question.Id
                             orderby question.IsSetted
                             select new TemasDTO()
                             {
                                 Title = question.Title,
                                 Description = question.Description,
                                 TotalResponse = question.Answers.Count(),
                             };
                return result.ToList();
            }
            return NotFound();

        }

        [Authorize]
        [HttpGet("myuser")]
        public IActionResult GetMyUser()
        {
            IActionResult response = NotFound();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            User user = FindUserByToken(identity);
            if (user != null)
            {
                response = Ok( new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    RoleName = user.RoleName,
                });
            }

            return response;
        }
        #endregion
        /// <summary>
        /// Crea nuevos modulos
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        #region Post
        [Authorize(Roles = "Administrador")]
        [HttpPost("temas")]
        public async Task<IActionResult> CreateModule(ModuleDTO dto) 
        {
            // Se da response badrequest para eviar poner muchos returns
            IActionResult response = BadRequest(new MessageDTO { Message="Modelo no válido comprueba los datos introducidos "});
            // Se comprueba ModelState
            if (ModelState.IsValid)
            {
                Module module = new Module()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    CourseId = dto.CourseId,
                };

                // Se añade
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();

                response = Ok(module);
            }
            return response;
        }

        #endregion

        #region Put
        /// <summary>
        /// Modifica un módulo
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("temas/{moduleId}")]
        public async Task<IActionResult> ModifyModule([FromRoute]int moduleId, [FromBody] Module module)
        {
            IActionResult response = NotFound();
            if(moduleId == module.Id)
            {
                if(_context.Modules.Any(x => x.Id == moduleId))
                {
                    _context.Entry(module).State = EntityState.Modified;
                }
                try
                {
                    await _context.SaveChangesAsync();
                    response= NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return response;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("temas/{questionId}/setted")]
        public async Task<IActionResult> SettedQuestion(int questionId, Question question)
        {
            IActionResult response = NotFound();
            if(ModelState.IsValid && question.Id == questionId)
            {
                if (_context.Questions.Any(x => x.Id == questionId))
                {
                    question.IsSetted = !question.IsSetted;
                    _context.Entry(question).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    response = NoContent();
                }
            }
            return response;
        }
        #endregion

        /// <summary>
        /// Devuelve un usuario según su claimIdentity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns>User</returns>
        private User FindUserByToken(ClaimsIdentity identity)
        {
            IList<Claim> claim = identity.Claims.ToList();
            return _context.Users.Where(x => x.Email == claim[1].Value).FirstOrDefault();
        }

        private bool ModuleExists (int moduleId)
        {
            return _context.Modules.Any(c => c.Id == moduleId);
        }
    }

}
