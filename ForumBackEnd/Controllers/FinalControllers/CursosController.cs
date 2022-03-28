using ForumBackEnd.Data;
using ForumBackEnd.DTO;
using ForumBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForumBackEnd.Controllers.FinalControllers
{
    [Route("/foro/cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ForumBackEndContext _context;

        public CursosController(ForumBackEndContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para leer el usuario devolviendo los cursos a los que está suscrito
        /// En caso de que el usuario no sea válido devuelve un NotFound
        /// </summary>
        /// <returns>List<Course>></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetUserCourse()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            User user =await _context.Users.Where(x => x.Email == claim[1].Value).FirstOrDefaultAsync();
            if (user != null)
            {
                var userCourse = from relation in _context.CourseRelations
                                 join course in _context.Courses on relation.CourseId equals course.Id
                                 where relation.UserId == user.Id
                                 select new Course()
                                 {
                                     Id = course.Id,
                                     Name = course.Name,
                                     Description = course.Description,
                                 };
                return userCourse.ToList();
            }
            return NotFound();
        }

        [Authorize (Roles = "Administrador")]
        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            List<Course> courses =await _context.Courses.ToListAsync();
            return courses;
        }

        /// <summary>
        /// Solo los administradores podrán crear nuevos cursos
        /// Método para crear cursos
        /// </summary>
        /// <param name="courseDTO"></param>
        /// <returns>IActionResult + model</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseDTO courseDTO)
        {
            IActionResult response = BadRequest(new MessageDTO { Message = "Modelo no válido comprueba los datos introducidos. " });
            // Comprobacion del model
            if (ModelState.IsValid)
            {
                // Se crea un nuevo Curso a partir del DTO
                Course course = new Course()
                {
                    Name = courseDTO.Name,
                    Description = courseDTO.Description,
                };

                // Se añade
                _context.Courses.Add(course);
                // Se guardan cambios en BBDD
                await _context.SaveChangesAsync();

                // cambiamos el valor a ok y devolvemos un curso
                response = Ok(course);
            }
            return response;
        }

        /// <summary>
        /// Solo los administradores pueden eliminar cursos
        /// Método para eliminar un curso pasanddo el id desde la ruta
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("{courseId}")]
        public async Task<IActionResult> EditCourse([FromRoute] int courseId, [FromBody] Course course)
        {
            IActionResult response = NotFound();
            if (!_context.Courses.Any(x => x.Id == course.Id))
            {
                _context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response = NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courseId))
                    {
                        response = NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return response;
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("/{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            IActionResult response = NotFound();
            Course course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                response = NoContent();
            }
            return response;
        }

        private bool CourseExists(int courseId)
        {
            return _context.Courses.Any(c => c.Id == courseId);
        }
    }

}
