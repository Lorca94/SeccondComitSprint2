

using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface IModuleRepository
    {
        IEnumerable<Module> GetModules();
        Module GetModule(int id);
        void InsertModule(Module module);
        void DeleteModule(int id);
        void UpdateModule(Module module);
        bool ModuleExist(int moduleId);

        void Save();
    }
}
