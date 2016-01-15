using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizRepository : IQuizRepository
    {
        public List<Quiz> GetQuizzes()
        {
            return QuizlrContext.Current.Quizzes.ToList();
        }

        public Quiz GetQuiz(int quizId)
        {
            return QuizlrContext.Current.Quizzes.FirstOrDefault(o => o.QuizId == quizId);
        }

        public Quiz CreateQuiz(Quiz quiz)
        {
            var ctx = QuizlrContext.Current;
            ctx.Quizzes.Add(quiz);
            ctx.SaveChanges();
            return quiz;
        }

        public void UpdateQuiz(Quiz quiz)
        {
            var ctx = QuizlrContext.Current;
            ctx.Quizzes.Attach(quiz);
            ctx.Entry(quiz).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteQuiz(Quiz quiz)
        {
            var ctx = QuizlrContext.Current;
            ctx.Quizzes.Remove(quiz);
            ctx.SaveChanges();
        }
    }
}