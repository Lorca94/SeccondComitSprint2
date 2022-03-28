using ForumBackEnd.Models;

namespace ForumBackEnd.Services.CourseRepository
{
    public interface ICourseRepository
    {
        IEnumerable<Course> FindAllCourses();
        Course FindCourseById (int courseId);
        Course CreateCourse(Course course);
        void UpdateCourse(Course course);
        bool DeleteCourse(int courseId);
        bool CourseExists(int courseId);
    }
}
