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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoursesController : ControllerBase
    {
        private CourseServices service;
        private UserServices userServices;

        public CoursesController(CourseServices service, UserServices userServices)
        {
            this.service = service;
            this.userServices = userServices;
        }

        // GET: api/Courses
        [HttpGet]
        public IEnumerable<Course> GetCourses()
        {
            return service.FindAllCourses();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            Course course = service.FindCourseById(id);

            if (course == null)
            {
                return NotFound(new MessageDTO {Message = "No se ha encontrado el curso"});
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, CourseDTO course)
        {
            Course actualCourse = service.FindCourseById(id);
            if (actualCourse == null)
            {
                return BadRequest(new MessageDTO { Message = "No se ha podido modificar con éxito"});
            }
            if(!actualCourse.Name.Equals(course.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                actualCourse.Name = course.Name;
            }
            if(!actualCourse.Description.Equals(course.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                actualCourse.Description = course.Description;
            }
            service.ModifyCourse(actualCourse);
            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Course> PostCourse(CourseDTO courseDTO)
        {
            Course course = new Course() { Name = courseDTO.Name, Description = courseDTO.Description, UserId = courseDTO.UserId };
            if (userServices.ExistsUser(course.UserId)) {
                if (service.ExistsByName(course.Name))
                {
                    return BadRequest(new MessageDTO {  Message = "Este curso ya ha sido registrado con anterioridad" });
                }
                service.CreateCourse(course);
                return Ok(new MessageDTO { Message = "Curso registrado  --> " + course.Name + "" });
            }
            return BadRequest(new MessageDTO { Message = "Necesitas un usuario válido para crear un curso" });
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            Course course = service.FindCourseById(id);
            if (course == null)
            {
                return NotFound(course);
            }

            service.DeteleCourse(id);
            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return service.ExistsById(id);
        }
    }
}
