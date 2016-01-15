using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuestionRepository : IQuestionRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public Question GetQuestion(int questionId)
        {
            return _context.Questions.FirstOrDefault(o => o.QuestionId == questionId);
        }

        public Question CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question;
        }

        public void UpdateQuestion(Question question)
        {
            _context.Questions.Attach(question);
            _context.Entry(question).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuestion(Question question)
        {
            _context.Questions.Remove(question);
        }
    }
}