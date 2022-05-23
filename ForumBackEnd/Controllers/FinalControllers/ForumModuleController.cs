using ForumBackEnd.Controllers.FinalControllers;
using ForumBackEnd.Data;
using ForumBackEnd.DTO;
using ForumBackEnd.Models;
using ForumBackEnd.Services.ModuleRepository;
using ForumBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumBackEnd.Controllers
{
    [Route("/forum/module")]
    [ApiController]
    public class ForumModuleController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ForumBackEndContext _context;

        public ForumModuleController(IUserRepository userRepository, IModuleRepository moduleRepository, ForumBackEndContext context)
        {
            _userRepository = userRepository;
            _moduleRepository = moduleRepository;
            _context = context;
        }

        /**
         * Devuelve los modulos suscritos de un usuario registrado
         * Solo pueden acceder usuarios registrados
         */
        [Authorize]
        [HttpGet("/myuser")]
        public async Task<ActionResult<IEnumerable<Module>>> GetUserModules()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            User user = _userRepository.FindByEmail(claim[1].Value);
            if (user != null)
            {
                var modules = from relation in _context.ModuleUser
                              join module in _context.Modules on relation.ModuleId equals module.Id
                              where relation.UserId == user.Id
                              select new Module()
                              {
                                  Id = module.Id,
                                  Name = module.Name,
                                  Description = module.Description,
                              };
                return modules.ToList();
            }
            return NotFound();
        }

        /**
         * Devuelve todos los modulos disponibles
         * Solo pueden acceder los administradores
         */

        [Authorize(Roles = "Administrador")]
        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModule()
        {

            return Ok(await _moduleRepository.FindAllModules());
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult> CreateModule(ModuleDTO moduleDTO)
        {
            ActionResult response = BadRequest(new MessageDTO { Message = "Comprueba los datos introducidos" });
            if (ModelState.IsValid)
            {
                Module module = new Module()
                {
                    Name = moduleDTO.Name,
                    Description = moduleDTO.Description,
                };
                _moduleRepository.CreateModule(module);
                response = Ok(module);
            }
            return response;
        }
    }
}
