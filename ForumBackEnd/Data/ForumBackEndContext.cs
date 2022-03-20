#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<ForumBackEnd.Models.Role> Roles { get; set; }
        public DbSet<ForumBackEnd.Models.Course> Courses { get; set; }
        public DbSet<ForumBackEnd.Models.Module> Modules { get; set; }
        public DbSet<ForumBackEnd.Models.Question> Questions{ get; set; }
        public DbSet<ForumBackEnd.Models.Answer> Answers{ get; set; }
        // public DbSet<ForumBackEnd.Models.Liked> Likeds{ get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // Role --> User Ok
            model.Entity<Role>().HasMany(r => r.Users).WithOne(u => u.Role).HasForeignKey(r => r.RoleId);
            //model.Entity<Role>().Navigation(b => b.Users).UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
            // User --> Course Ok
            model.Entity<User>().HasMany(r => r.Courses).WithOne(u => u.User).HasForeignKey(r => r.UserId);
            // Course --> module Ok
            model.Entity<Course>().HasMany(c => c.Modules).WithOne(m => m.Course).HasForeignKey(c => c.CourseId);
            // Course User ok
            model.Entity<Course>().HasOne(c => c.User).WithMany(c => c.Courses).HasForeignKey(c => c.UserId);
            // ModuleUser ok
            model.Entity<Module>().HasOne(m => m.User).WithMany(u => u.Modules).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
            // ModuleQuestion ok
            model.Entity<Module>().HasMany(m => m.Questions).WithOne(q => q.Module).HasForeignKey(r => r.ModuleId);
            // Question User ok
            model.Entity<Question>().HasOne(q => q.User).WithMany(u => u.Questions).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
            // Answer User ok
            model.Entity<Answer>().HasOne(q => q.User).WithMany(u => u.Answers).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
            // Answer Question ok
            model.Entity<Answer>().HasOne(a => a.Question).WithMany(u => u.Answers).HasForeignKey(r => r.QuestionId);
            // like user
            model.Entity<User>().HasMany(a => a.Likes).WithOne(a => a.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
           // question like
            model.Entity<Question>().HasMany(a => a.Like).WithOne(b => b.Question).HasForeignKey(b => b.QuestionId).OnDelete(DeleteBehavior.NoAction);
            // suscriber user
            model.Entity<User>().HasMany(a => a.Subs).WithOne(a => a.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.NoAction);
            // siscriber question
            model.Entity<Question>().HasMany(a => a.Subs).WithOne(b => b.Question).HasForeignKey(b => b.QuestionId).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(model);
        }
        // public DbSet<ForumBackEnd.Models.Liked> Likeds{ get; set; }

        public DbSet<ForumBackEnd.Models.Like> Like { get; set; }
        // public DbSet<ForumBackEnd.Models.Liked> Likeds{ get; set; }

        public DbSet<ForumBackEnd.Models.Suscriber> Suscriber { get; set; }
    }
}
