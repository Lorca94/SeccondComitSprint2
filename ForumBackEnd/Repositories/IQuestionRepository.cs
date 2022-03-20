using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface IQuestionRepository: IDisposable
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestion(int questionId);
        void InsertQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(Question question);
        bool QuestionExists(int questionId);
        void Save();
    }
}
