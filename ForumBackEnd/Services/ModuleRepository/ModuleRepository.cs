using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Services.ModuleRepository
{
    public class ModuleRepository: IModuleRepository
    {
        private readonly ForumBackEndContext _context;

        public ModuleRepository (ForumBackEndContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> FindAllModules()
        {
            return await _context.Modules.ToListAsync();
        }

        public async Task<Module> FindById(int moduleId)
        {
            return await _context.Modules.FindAsync(moduleId);
        }

        public async Task<Module> CreateModule(Module module)
        {
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return module;
        }

        public async void UpdateModule(Module module)
        {
            _context.Entry(module).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool DeleteModule(int moduleId)
        {
            Module module = _context.Modules.Find(moduleId);
            if( module == null)
            {
                return false;
            }
            _context.Modules.Remove(module);
            _context.SaveChanges();
            return true;
        }

        public bool ModuleExists(int moduleId)
        {
            return _context.Modules.Any(module => module.Id == moduleId);
        }
    }
}
