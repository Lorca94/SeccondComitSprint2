using ForumBackEnd.Models;
using ForumBackEnd.Repositories;

namespace ForumBackEnd.Services
{
    public class AnswerServices
    {
        private IAnswerRepository answerRepository;
        private IQuestionRepository questionRepository;
        private IUserRepository userRepository;

        public AnswerServices(IAnswerRepository answerRepository, IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<Answer> FindAllAnswers()
        {
            return answerRepository.GetAllAnswers();
        }

        public Answer FindAnswerById(int id)
        {
            return answerRepository.GetAnswerById(id);
        }

        public bool CreateAnswer(Answer answer)
        {
            if (!questionRepository.QuestionExists(answer.QuestionId))
            {
                return false;
            }
            if (!userRepository.UserExists(answer.UserId))
            {
                return false;
            }
            answerRepository.InsertAnswer(answer);
            answerRepository.Save();
            return true;
        }

        public bool ModifyAnswer(Answer answer)
        {
            if (answerRepository.AnswerExists(answer.Id))
            {
                answerRepository.UpdateAnswer(answer);
                answerRepository.Save();
                return true;
            }
            return false;
        }

        public void DeteleAnswer(Answer answer)
        {
           answerRepository.DeteleAnswer(answer);
        }

        public bool ExistByid(int id)
        {
            return answerRepository.AnswerExists(id);
        }
    }
}
