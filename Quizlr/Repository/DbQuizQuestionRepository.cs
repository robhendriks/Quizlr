using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizQuestionRepository : IQuizQuestionRepository
    {
        public List<QuizQuestion> GetQuizQuestions()
        {
            return QuizlrContext.Current.QuizQuestions.ToList();
        }

        public QuizQuestion GetQuizQuestion(int quizQuestionId)
        {
            return QuizlrContext.Current.QuizQuestions.FirstOrDefault(o => o.QuizQuestionId == quizQuestionId);
        }

        public QuizQuestion GetByQuizIdAndQuestionId(int quizId, int questionId)
        {
            return
                QuizlrContext.Current.QuizQuestions.FirstOrDefault(o => o.QuizId == quizId && o.QuestionId == questionId);
        }

        public QuizQuestion CreateQuizQuestion(QuizQuestion quizQuestion)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizQuestions.Add(quizQuestion);
            ctx.SaveChanges();
            return quizQuestion;
        }

        public void UpdateQuizQuestion(QuizQuestion quizQuestion)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizQuestions.Attach(quizQuestion);
            ctx.Entry(quizQuestion).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteQuizQuestion(QuizQuestion quizQuestion)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizQuestions.Remove(quizQuestion);
            ctx.SaveChanges();
        }
    }
}