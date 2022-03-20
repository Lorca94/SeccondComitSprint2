using ForumBackEnd.Data;
using ForumBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumBackEnd.Repositories
{
    public class AnswerRepository: IAnswerRepository, IDisposable
    {
        private ForumBackEndContext context;

        public AnswerRepository(ForumBackEndContext context)
        {
            this.context = context;
        }

        public IEnumerable<Answer> GetAllAnswers()
        {
            return context.Answers.ToList();
        }

        public Answer GetAnswerById(int id)
        {
            return context.Answers.Find(id);
        }

        public void InsertAnswer(Answer answer)
        {
            context.Answers.Add(answer);
        }

        public void UpdateAnswer(Answer answer)
        {
            context.Entry(answer).State = EntityState.Modified;
        }

        public void DeteleAnswer(Answer answer)
        {
            context.Answers.Remove(answer);
        }

        public bool AnswerExists(int id)
        {
            return context.Answers.Any(x => x.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
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
