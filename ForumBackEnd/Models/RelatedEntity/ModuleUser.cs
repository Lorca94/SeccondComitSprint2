using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class ModuleUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; } 
        public virtual User User { get; set; }  
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}
