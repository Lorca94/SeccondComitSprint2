namespace ForumBackEnd.DTO
{
    public class AnswerDTO
    {
        public string Response { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
