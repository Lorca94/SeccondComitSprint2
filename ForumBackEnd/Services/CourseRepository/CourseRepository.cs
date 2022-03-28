using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Services.CourseRepository
{
    public class CourseRepository: ICourseRepository
    {
        private readonly ForumBackEndContext context;

        public CourseRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        public IEnumerable<Course> FindAllCourses()
        {
            return context.Courses.ToList();
        }

        public Course FindCourseById(int courseId)
        {
            return context.Courses.Find(courseId);
        }

        public Course CreateCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
            return course;
        }

        public void UpdateCourse(Course course)
        {
            context.Entry(course).State = EntityState.Modified;
        }

        public bool DeleteCourse(int CourseId)
        {
            var course = context.Courses.Find(CourseId);
            if(course == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CourseExists(int courseId)
        {
            return context.CourseRelations.Any(r => r.CourseId == courseId);
        }
    }
}
