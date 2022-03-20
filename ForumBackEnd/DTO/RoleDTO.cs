using System.ComponentModel.DataAnnotations;

namespace ForumBackEnd.DTO
{
    public class RoleDTO
    {
        [Required]
        public string RoleType { get; set; }
    }
}
