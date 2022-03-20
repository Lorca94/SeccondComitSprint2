using ForumBackEnd.Models;

namespace ForumBackEnd.Repositories
{
    public interface IAnswerRepository: IDisposable
    {
        IEnumerable<Answer> GetAllAnswers();
        Answer GetAnswerById(int id);
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeteleAnswer(Answer answer);
        bool AnswerExists(int id);
        void Save();
    }
}
