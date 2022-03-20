namespace ForumBackEnd.DTO
{
    public class LikedDTO
    {
        public bool isLiked { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
