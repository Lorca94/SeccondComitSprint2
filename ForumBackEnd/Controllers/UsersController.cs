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
using ForumBackEnd.Services.UserRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ForumBackEnd.Controllers.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ForumBackEnd.Services.PasswordRepository;
using Microsoft.AspNetCore.Authorization;

namespace ForumBackEnd.Controllers
{
    [Route("forum/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _enconder;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository userRepository, IPasswordRepository enconder,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _enconder = enconder;
            _configuration = configuration;
        }


        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_userRepository.FindAllUsers());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.FindById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            if (!UserExists(user.Id))
            {
                return NotFound();
            }

            _userRepository.Update(user);
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_userRepository.DeleteById(id))
            {
                return NoContent();
            } else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Permite crear usuarios con Role "Usuario"
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User existingUserByEmail = _userRepository.FindByEmail(register.Email);
            if (existingUserByEmail != null)
            {
                return Conflict();
            }
            if (register.Password != register.ConfirmPassword)
            {
                return BadRequest();
            }
            string passwordHash = _enconder.HashPassword(register.Password);
            User user = new User()
            {
                Email = register.Email,
                Username = register.UserName,
                Password = passwordHash,
                RoleName = register.RoleName
            };
            _userRepository.Create(user);
            return Ok(user);
        }
       

        private bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }
    }
}
