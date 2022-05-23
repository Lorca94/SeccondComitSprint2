using ForumBackEnd.Models;

namespace ForumBackEnd.Services.ModuleRepository
{
    public interface IModuleRepository 
    {
        Task<IEnumerable<Module>> FindAllModules();
        Task<Module> FindById(int moduleId);
        Task<Module> CreateModule(Module module);
        void UpdateModule(Module module);
        bool DeleteModule(int moduleId);
        bool ModuleExists(int moduleId);
    }
}
