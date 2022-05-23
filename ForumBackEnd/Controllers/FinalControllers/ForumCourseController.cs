using ForumBackEnd.Data;
using ForumBackEnd.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services.CourseRepository;
using ForumBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForumBackEnd.Controllers
{
    [Route("/forum/course")]
    [ApiController]
    public class ForumCourseController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ForumBackEndContext _context;
        private readonly ICourseRepository _courseRepository;

        public ForumCourseController(IUserRepository userRepository, ForumBackEndContext context, ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _courseRepository = courseRepository;
        }

        /**
         * Devuelve los cursos suscritos de un usuario registrado
         * Solo pueden acceder usuarios registrados
         */
        [Authorize]
        [HttpGet("/myuser")]
        public async Task<ActionResult<IEnumerable<Course>>> GetUserCourse()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            User user = _userRepository.FindByEmail(claim[1].Value);
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

        /**
         * Devuelve todos los cursos disponibles
         * Solo puede acceder un rol de Administrador
         */
        [Authorize(Roles = "Administrador")]
        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            List<Course> courses = await _context.Courses.ToListAsync();
            return courses;
        }

        /**
         * Crea un nuevo curso
         * Solo puede acceder un rol de Administrador
         */
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(CourseDTO courseDTO)
        {
            ActionResult response = BadRequest(new MessageDTO { Message = "Modelo inválido comprueba los datos introducidos" });
            if (ModelState.IsValid)
            {
                Course course = new Course()
                {
                    Name = courseDTO.Name,
                    Description = courseDTO.Description,
                };
                _courseRepository.CreateCourse(course);
                response = Ok(course);
            }
            return response;
        }

        /**
         * Edita un curso
         * Solo puede acceder un rol de Administrador
         */
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest(new MessageDTO { Message = "La id introducida y el curso son distintos" });
            }
            _courseRepository.UpdateCourse(course);
            return NoContent();
        }

        /**
         * Elimina un curso
         * Solo puede acceder un rol de Administrador
         */
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (!_courseRepository.CourseExists(id))
            {
                return NotFound();
            }
            _courseRepository.DeleteCourse(id);
            return NoContent();
        }
    }

}
