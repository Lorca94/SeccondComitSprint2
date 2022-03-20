using ForumBackEnd.Models;
using ForumBackEnd.Repositories;

namespace ForumBackEnd.Services
{
    public class ModuleServices
    {
        // Se instancia las repos
        private IUserRepository userRepository;
        private IModuleRepository moduleRepository;
        private ICourseRepository courseRepository;

        public ModuleServices(IUserRepository userRepository, IModuleRepository moduleRepository, ICourseRepository courseRepository)
        {
            this.userRepository = userRepository;
            this.moduleRepository = moduleRepository;
            this.courseRepository = courseRepository;
        }

        public IEnumerable<Module> FindModules()
        {
            return moduleRepository.GetModules();
        }

        public Module FindModule(int moduleId)
        {
            if (moduleId < 0)
            {
                return null;
            }
            return moduleRepository.GetModule(moduleId);
        }

        public bool CreateModule(Module module)
        {
            if (userRepository.UserExists(module.UserId))
            {
                if (courseRepository.CourseExists(module.CourseId))
                {
                    moduleRepository.InsertModule(module);
                    moduleRepository.Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool ModifyModule(Module module)
        {
            if (moduleRepository.ModuleExist(module.Id))
            {
                moduleRepository.UpdateModule(module);
                moduleRepository.Save();
                return true;
            }
            return false;
        }

        public void DeleteModule(int moduleId)
        {
            moduleRepository.DeleteModule(moduleId);
            moduleRepository.Save();
        }

        public bool ExistById(int moduleId)
        {
            return moduleRepository.ModuleExist(moduleId);
        }

        public void Save()
        {
            moduleRepository.Save();
        }
    }
}
