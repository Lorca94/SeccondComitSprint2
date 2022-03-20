using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class ModuleRepository : IModuleRepository, IDisposable
    {
        private ForumBackEndContext context;

        public ModuleRepository(ForumBackEndContext context)
        {
            this.context = context;
        }


        public IEnumerable<Module> GetModules()
        {
            return context.Modules.ToList();
        }

        public Module GetModule(int id)
        {
            return context.Modules.Find(id);
        }

        public void InsertModule(Module module)
        {
            context.Modules.Add(module);
        }

        public void DeleteModule(int id)
        {
            var module = context.Modules.Find(id);
            context.Modules.Remove(module);
        }

        public void UpdateModule(Module module)
        {
            context.Entry(module).State = EntityState.Modified;
        }

        public bool ModuleExist(int moduleId)
        {
            return context.Roles.Any(module => module.Id == moduleId);
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
