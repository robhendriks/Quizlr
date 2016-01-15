using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbAnswerRepository : IAnswerRepository
    {
        public List<Answer> GetAnswers()
        {
            return QuizlrContext.Current.Answers.ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            return QuizlrContext.Current.Answers.FirstOrDefault(o => o.AnswerId == answerId);
        }

        public Answer CreateAnswer(Answer answer)
        {
            var ctx = QuizlrContext.Current;
            ctx.Answers.Add(answer);
            ctx.SaveChanges();
            return answer;
        }

        public void UpdateAnswer(Answer answer)
        {
            var ctx = QuizlrContext.Current;
            ctx.Answers.Attach(answer);
            ctx.Entry(answer).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteAnswer(Answer answer)
        {
            var ctx = QuizlrContext.Current;
            ctx.Answers.Remove(answer);
            ctx.SaveChanges();
        }
    }
}