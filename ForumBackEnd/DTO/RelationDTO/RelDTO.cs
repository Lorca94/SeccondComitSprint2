using System.ComponentModel.DataAnnotations;

namespace ForumBackEnd.DTO.RelationDTO
{
    public class RelDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RelId { get; set; }
    }
}
