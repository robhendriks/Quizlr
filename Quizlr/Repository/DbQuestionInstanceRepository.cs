using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuestionInstanceRepository : IQuestionInstanceRepository
    {
        public List<QuestionInstance> GetInstances()
        {
            return QuizlrContext.Current.QuestionInstances.ToList();
        }

        public QuestionInstance GetInstance(int instanceId)
        {
            return QuizlrContext.Current.QuestionInstances.FirstOrDefault(o => o.QuestionInstanceId == instanceId);
        }

        public QuestionInstance CreateInstance(QuestionInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuestionInstances.Add(instance);
            ctx.SaveChanges();
            return instance;
        }

        public void UpdateInstance(QuestionInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuestionInstances.Attach(instance);
            ctx.Entry(instance).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteInstance(QuestionInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuestionInstances.Remove(instance);
            ctx.SaveChanges();
        }
    }
}