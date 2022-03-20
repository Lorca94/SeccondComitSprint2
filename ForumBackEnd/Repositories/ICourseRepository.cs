using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface ICourseRepository: IDisposable
    {
        IEnumerable<Course> GetCourses();
        Course GetCourse(int id);
        Course GetCourseByName(string name);
        void InsertCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        bool CourseExists(int id);

        void Save();
        
    }
}
