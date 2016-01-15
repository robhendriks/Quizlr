using System.Collections.Generic;
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

        private class QuizlrInitializer : DropCreateDatabaseIfModelChanges<QuizlrContext>
        {
            protected override void Seed(QuizlrContext context)
            {
                var quizzes = new List<Quiz>
                {
                    new Quiz {QuizId = 1, Name = "Quiz van Klaas"},
                    new Quiz {QuizId = 2, Name = "Quiz van Pieter"}
                };
                context.Quizzes.AddRange(quizzes);
                var categories = new List<Category>
                {
                    new Category {Name = "Taal"},
                    new Category {Name = "Wiskunde"},
                    new Category {Name = "Geschiedenis"},
                    new Category {Name = "Aardrijkskunde"}
                };
                context.Categories.AddRange(categories);
                var questions = new List<Question>
                {
                    new Question {Text = "Welke woorden zijn lidwoorden?", Category = categories[0]},
                    new Question {Text = "Wat is de wortel van 25?", Category = categories[1]},
                    new Question {Text = "In welk jaar begon de Tweede Wereldoorlog?", Category = categories[2]},
                    new Question {Text = "Wat is de hoofdstad van Colombia?", Category = categories[3]}
                };
                context.Questions.AddRange(questions);
                var quizQuestions = new List<QuizQuestion>
                {
                    new QuizQuestion {Quiz = quizzes[0], Question = questions[0]},
                    new QuizQuestion {Quiz = quizzes[0], Question = questions[1]},
                    new QuizQuestion {Quiz = quizzes[1], Question = questions[2]},
                    new QuizQuestion {Quiz = quizzes[1], Question = questions[3]}
                };
                context.QuizQuestions.AddRange(quizQuestions);
                var answers = new List<Answer>
                {
                    new Answer {Text = "De", IsCorrect = true, Question = questions[0]},
                    new Answer {Text = "Die", IsCorrect = false, Question = questions[0]},
                    new Answer {Text = "5", IsCorrect = true, Question = questions[1]},
                    new Answer {Text = "10", IsCorrect = false, Question = questions[1]},
                    new Answer {Text = "1939", IsCorrect = true, Question = questions[2]},
                    new Answer {Text = "1945", IsCorrect = false, Question = questions[2]},
                    new Answer {Text = "Bogota", IsCorrect = true, Question = questions[3]},
                    new Answer {Text = "Quito", IsCorrect = false, Question = questions[3]}
                };
                context.Answers.AddRange(answers);
                context.SaveChanges();
            }
        }
    }
}