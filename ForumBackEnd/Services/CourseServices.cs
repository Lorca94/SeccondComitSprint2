using ForumBackEnd.Models;
using ForumBackEnd.Repositories;

namespace ForumBackEnd.Services
{
    public class CourseServices
    {
        private ICourseRepository courseRepository;
        private IUserRepository userRepository;

        public CourseServices(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<Course> FindAllCourses()
        {
            return courseRepository.GetCourses();
        }

        public Course FindCourseById(int courseId)
        {
            if(courseId < 0)
            {
                return null;
            } 
            return courseRepository.GetCourse(courseId);
        }

        public Course FindCourseByName(string name)
        {
            return courseRepository.GetCourseByName(name);
        }

        public bool CreateCourse(Course course)
        {
            if (!userRepository.UserExists(course.UserId))
            {
                return false;
            }
            courseRepository.InsertCourse(course);
            courseRepository.Save();
            return true;
        }

        public bool ModifyCourse(Course course)
        {
            if (courseRepository.CourseExists(course.Id))
            {

                courseRepository.UpdateCourse(course);
                courseRepository.Save();
                return true;
            }
            return false;
        }

        public void DeteleCourse(int id)
        {
            courseRepository.DeleteCourse(id);
            courseRepository.Save();
        }

        public bool ExistsById(int id)
        {
            return courseRepository.CourseExists(id);
        }
        public bool ExistsByName(string name)
        {
            Course course = courseRepository.GetCourseByName(name);
            if (course == null)
            {
                return false;
            }
            return true;
        }
    }
}
