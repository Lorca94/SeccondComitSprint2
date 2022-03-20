 using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class QuestionsRepository: IQuestionRepository, IDisposable
    {
        private ForumBackEndContext context;

        public QuestionsRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return context.Questions.ToList();
        }

        public Question GetQuestion(int questionId)
        {
            return context.Questions.Find(questionId);
        }

        public void InsertQuestion(Question question)
        {
            context.Questions.Add(question);
        }

        public void UpdateQuestion(Question question)
        {
            context.Entry(question).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void DeleteQuestion(Question question)
        {
            context.Remove(question);
        }

        public bool QuestionExists(int questionId)
        {
            return context.Questions.Any(q => q.Id == questionId);
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
