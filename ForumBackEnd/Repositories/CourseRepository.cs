using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class CourseRepository: ICourseRepository, IDisposable
    {
        private ForumBackEndContext context;

        public CourseRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        public IEnumerable<Course> GetCourses()
        {
            return context.Courses.ToList();
        }

        public Course GetCourse(int id)
        {
            return context.Courses.Find(id);
        }

        public Course GetCourseByName(string name)
        {
            return context.Courses.Where(c => c.Name == name).FirstOrDefault();
        }

        public void InsertCourse(Course course)
        {
            context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            context.Entry(course).State = EntityState.Modified;
        }

        public bool CourseExists(int id)
        {
            return context.Courses.Any(c => c.Id == id);
        }

        public void DeleteCourse(int id)
        {
            Course course = context.Courses.Find(id);
            context.Courses.Remove(course);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
         }
    }
}
