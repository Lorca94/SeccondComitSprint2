using System.ComponentModel.DataAnnotations;

namespace ForumBackEnd.Controllers.DTO
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string RoleName{ get; set; }
    }
}
