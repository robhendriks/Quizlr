using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbAnswerRepository : IAnswerRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Answer> GetAnswers()
        {
            return _context.Answers.ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            return _context.Answers.FirstOrDefault(o => o.AnswerId == answerId);
        }

        public Answer CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
            return answer;
        }

        public void UpdateAnswer(Answer answer)
        {
            _context.Answers.Attach(answer);
            _context.Entry(answer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAnswer(Answer answer)
        {
            _context.Answers.Remove(answer);
            _context.SaveChanges();
        }
    }
}