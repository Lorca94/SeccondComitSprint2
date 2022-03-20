using ForumBackEnd.Models;
using ForumBackEnd.Repositories;

namespace ForumBackEnd.Services
{
    public class QuestionServices
    {
        private IModuleRepository moduleRepository;
        private IQuestionRepository questionRepository;
        private IUserRepository userRepository;

        public QuestionServices(IModuleRepository moduleRepository, IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            this.moduleRepository = moduleRepository;
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<Question> FindAllQuestions()
        {
            return questionRepository.GetAllQuestions();
        }

        public Question FindQuestionById(int moduleId)
        {
            return questionRepository.GetQuestion(moduleId);
        }

        public bool CreateQuestion(Question question)
        {
            if (!moduleRepository.ModuleExist(question.ModuleId))
            {
                return false;
            }
            if (!userRepository.UserExists(question.UserId))
            {
                return false;
            }
            questionRepository.InsertQuestion(question);
            questionRepository.Save();
            return true;
        }

        public bool ModifyQuestion(Question question)
        {
            if (questionRepository.QuestionExists(question.Id))
            {
                questionRepository.UpdateQuestion(question);
                questionRepository.Save();
                return true;
            }
            return false;
        }


        public void DeleteQuestion(int questionId)
        {
            Question question = questionRepository.GetQuestion(questionId);
            questionRepository.DeleteQuestion(question);
            questionRepository.Save();
        }

        public bool ExistsById(int id)
        {
            return questionRepository.QuestionExists(id);
        }
    }
}
