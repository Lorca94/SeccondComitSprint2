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
using ForumBackEnd.Controllers.DTO;
using System.Net;
using ForumBackEnd.DTO;

namespace ForumBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserServices userServices;
        private RoleServices roleServices;

        public UsersController(UserServices userServices, RoleServices roleServices)
        {
            this.userServices = userServices;
            this.roleServices = roleServices;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return userServices.FindAllUser();
        }
        
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return userServices.FindUserById(id);
        }

        [HttpPost]
        public IActionResult PostUser(UserDTO userDTO)
        {   
            if (ModelState.IsValid)
            {
                User user = new User() { Email = userDTO.Email, Username = userDTO.UserName, Password = userDTO.Password, RoleId = userDTO.RoleId };
                // Si el usuario es 
                if (userServices.ValidateUser(user))
                {
                    int proccess = userServices.CreateUser(user);
                    switch (proccess)
                    {
                        case -1:
                            return BadRequest( new MessageDTO { Message = "Necesitas un rol válido para crear un usuario"});
                        case 0:
                            return BadRequest(new MessageDTO { Message = "Email/Username en uso" });
                        case 1:
                            return Ok(new MessageDTO { Message = "Usuario creado con éxito" });
                    }
                }
            }
            return BadRequest(ModelState);
        }

        
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new MessageDTO { Message = "Id no corresponde con el usuario"});
            }
            if (!userServices.ModifyUser(user))
            {
                return BadRequest(new MessageDTO { Message = "No se ha encontrado el usuario a modificar" });
            }
            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = userServices.FindUserById(id);
            if(user != null)
            {
                return NoContent();
            }
            return NotFound(new MessageDTO { Message = "Usuario no encontrado"});
        }
    }
}
