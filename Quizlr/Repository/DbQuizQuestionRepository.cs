using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<QuizQuestion> GetQuizQuestions()
        {
            return _context.QuizQuestions.ToList();
        }

        public QuizQuestion GetQuizQuestion(int quizQuestionId)
        {
            return _context.QuizQuestions.FirstOrDefault(o => o.QuizQuestionId == quizQuestionId);
        }

        public QuizQuestion GetByQuizIdAndQuestionId(int quizId, int questionId)
        {
            return _context.QuizQuestions.FirstOrDefault(o => o.QuizId == quizId && o.QuestionId == questionId);
        }

        public QuizQuestion CreateQuizQuestion(QuizQuestion quizQuestion)
        {
            _context.QuizQuestions.Add(quizQuestion);
            _context.SaveChanges();
            return quizQuestion;
        }

        public void UpdateQuizQuestion(QuizQuestion quizQuestion)
        {
            _context.QuizQuestions.Attach(quizQuestion);
            _context.Entry(quizQuestion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuizQuestion(QuizQuestion quizQuestion)
        {
            _context.QuizQuestions.Remove(quizQuestion);
            _context.SaveChanges();
        }
    }
}