using System.Data.Entity;
using Quizlr.Domain.Model;

namespace Quizlr.Repository
{
    public class QuizlrContext : DbContext
    {
        static QuizlrContext()
        {
            Database.SetInitializer(new QuizlrInitializer());
        }

        public QuizlrContext() : base("name=Quizlr")
        {
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        private class QuizlrInitializer : DropCreateDatabaseIfModelChanges<DbContext>
        {
        }
    }
}