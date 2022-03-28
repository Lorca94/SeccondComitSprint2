#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ForumBackEnd.Models;
using ForumBackEnd.Models;  

namespace ForumBackEnd.Data
{
    public class ForumBackEndContext : DbContext
    {
        public ForumBackEndContext (DbContextOptions<ForumBackEndContext> options)
            : base(options)
        {
        }
        public DbSet<ForumBackEnd.Models.User> Users { get; set; }
        public DbSet<ForumBackEnd.Models.Role> Roles  { get; set; }
        public DbSet<ForumBackEnd.Models.Course> Courses { get; set; }
        public DbSet<ForumBackEnd.Models.CourseUser> CourseRelations { get; set; }
        public DbSet<ForumBackEnd.Models.Module> Modules { get; set; }
        public DbSet<ForumBackEnd.Models.ModuleUser> ModuleUser { get; set; }
        public DbSet<ForumBackEnd.Models.Question> Questions { get; set; }
        public DbSet<ForumBackEnd.Models.SubQuestion> SubQuestions { get; set; }
        public DbSet<ForumBackEnd.Models.LikesQuestion> LikesQuestions { get; set; }
        public DbSet<ForumBackEnd.Models.LikesAnswer> LikesAnswers { get; set; }

        public DbSet<ForumBackEnd.Models.Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            // Relacion course module
            model.Entity<Course>().HasMany( r => r.Modules ).WithOne( r => r.Course);

            // Relacion user course
            model.Entity<CourseUser>().HasKey( x => new { x.UserId, x.CourseId });

            // Relacion user module
            model.Entity<ModuleUser>().HasKey( x => new { x.UserId, x.ModuleId });

            // Relacion user question
            model.Entity<Question>().HasOne( x => x.User ).WithMany( u => u.Questions ).OnDelete(DeleteBehavior.NoAction); ; ;

            // Relacion question like
            model.Entity<LikesQuestion>().HasKey(x => new { x.UserId, x.QuestionId });

            // Relacion answer like
            model.Entity<LikesAnswer>().HasKey(x => new { x.UserId, x.AnswerId });
            // Relacion question answer
            model.Entity<Answer>().HasOne( x => x.Question ).WithMany( u => u.Answers ).OnDelete(DeleteBehavior.NoAction); ; ;

            // Relacion user answer
            model.Entity<Answer>().HasOne(x => x.User).WithMany(u => u.Answers).OnDelete(DeleteBehavior.NoAction); ;
            // Relacion de subsquestion
            model.Entity<SubQuestion>().HasKey(x => new { x.UserId, x.QuestionId });
            // Relacion Module Question
            model.Entity<Question>().HasOne(x => x.Module).WithMany( u => u.Questions ).OnDelete(DeleteBehavior.NoAction);
        }

        
    }
}
