using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuestionRepository : IQuestionRepository
    {
        public List<Question> GetQuestions()
        {
            return QuizlrContext.Current.Questions.ToList();
        }

        public Question GetQuestion(int questionId)
        {
            return QuizlrContext.Current.Questions.FirstOrDefault(o => o.QuestionId == questionId);
        }

        public Question CreateQuestion(Question question)
        {
            var ctx = QuizlrContext.Current;
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            return question;
        }

        public void UpdateQuestion(Question question)
        {
            var ctx = QuizlrContext.Current;
            ctx.Questions.Attach(question);
            ctx.Entry(question).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteQuestion(Question question)
        {
            var ctx = QuizlrContext.Current;
            ctx.Questions.Remove(question);
            ctx.SaveChanges();
        }
    }
}