using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizInstanceRepository : IQuizInstanceRepository
    {
        public List<QuizInstance> GetInstances()
        {
            return QuizlrContext.Current.QuizInstances.ToList();
        }

        public QuizInstance GetInstance(int instanceId)
        {
            return QuizlrContext.Current.QuizInstances.FirstOrDefault(o => o.QuizInstanceId == instanceId);
        }

        public QuizInstance CreateInstance(QuizInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizInstances.Add(instance);
            ctx.SaveChanges();
            return instance;
        }

        public void UpdateInstance(QuizInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizInstances.Attach(instance);
            ctx.Entry(instance).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteInstance(QuizInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.QuizInstances.Remove(instance);
            ctx.SaveChanges();
        }
    }
}