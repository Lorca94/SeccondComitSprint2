namespace ForumBackEnd.DTO
{
    public class AnswerDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
