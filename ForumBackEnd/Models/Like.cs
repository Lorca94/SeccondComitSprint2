namespace ForumBackEnd.Models
{
    public class Like
    {
        public int Id { get; set; }
        public bool isLiked { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer{ get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question{ get; set; }

        public int UserId { get; set; }
        public virtual User User{ get; set; }
    }
}
