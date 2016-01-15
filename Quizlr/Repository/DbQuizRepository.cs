using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizRepository : IQuizRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Quiz> GetQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public Quiz GetQuiz(int quizId)
        {
            return _context.Quizzes.FirstOrDefault(o => o.QuizId == quizId);
        }

        public Quiz CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _context.Quizzes.Attach(quiz);
            _context.Entry(quiz).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuiz(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
        }
    }
}