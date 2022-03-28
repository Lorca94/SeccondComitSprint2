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
using ForumBackEnd.Services.CourseRepository;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        // GET: api/Courses
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return Ok(courseRepository.FindAllCourses());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = courseRepository.FindCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            courseRepository.UpdateCourse(course);
            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Course> PostCourse(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course()
                {
                    Name = courseDTO.Name,
                    Description = courseDTO.Description
                };
                courseRepository.CreateCourse(course);
                return Ok(course);
            }
            return BadRequest();

            
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (!courseRepository.DeleteCourse(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return courseRepository.CourseExists(id);
        }
    }
}
