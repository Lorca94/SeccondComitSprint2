using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumBackEnd.Models
{
    public class LikesAnswer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool isLike { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer{ get; set; }
    }
}
