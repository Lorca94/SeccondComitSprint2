#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumBackEnd.Data;
using ForumBackEnd.Models;
using ForumBackEnd.Services;
using ForumBackEnd.DTO;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private UserServices userServices;
        private CourseServices courseServices;
        private ModuleServices moduleServices;

        public ModulesController(UserServices userServices, CourseServices courseServices, ModuleServices moduleServices) 
        {
            this.userServices = userServices;
            this.courseServices = courseServices;
            this.moduleServices = moduleServices;
        }

        // GET: api/Modules
        [HttpGet]
        public IEnumerable<Module> GetModules()
        {
            return moduleServices.FindModules();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public ActionResult<Module> GetModule(int id)
        {
            Module module = moduleServices.FindModule(id);

            if (@module == null)
            {
                return NotFound(new MessageDTO { Message = "No se ha encontrado el módulo"});
            }

            return module;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutModule(int id, Module @module)
        {
            Module actualModule = moduleServices.FindModule(id);
            if (actualModule == null)
            {
                return BadRequest();
            }
            if(!actualModule.Title.Equals(module.Title, StringComparison.InvariantCultureIgnoreCase))
            {
                actualModule.Title = module.Title;
            }
            if(!actualModule.Description.Equals(module.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                actualModule.Description = module.Description;
            }
            if (!actualModule.CourseId.Equals(module.CourseId))
            {
                actualModule.CourseId = module.CourseId;
            }
            moduleServices.ModifyModule(actualModule);
            moduleServices.Save();
            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Module> PostModule(ModuleDTO moduleDTO)
        {
            Module module = new Module() { 
                Title = moduleDTO.Title, 
                Description = moduleDTO.Description, 
                CourseId = moduleDTO.CourseId, 
                UserId = moduleDTO.UserId 
            };
            if (userServices.ExistsUser(module.UserId))
            {
                if (courseServices.ExistsById(module.CourseId))
                {
                    moduleServices.CreateModule(module);
                    moduleServices.Save();
                    return Ok(new MessageDTO { Message = "Modulo añadido con éxito" });
                }
                return BadRequest(new MessageDTO { Message = "Necesitas un curso válido para crear un módulo"});
            }
            return BadRequest(new MessageDTO { Message = "Necesitas un usuario válido para registrar el módulo"});

            
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public IActionResult DeleteModule(int id)
        {
            Module module = moduleServices.FindModule(id);
            if (module == null)
            {
                return NotFound();
            }

            moduleServices.DeleteModule(id);
            moduleServices.Save();

            return NoContent();
        }
    }
}
