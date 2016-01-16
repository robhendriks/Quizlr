using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbAnswerInstanceRepository : IAnswerInstanceRepository
    {
        public List<AnswerInstance> GetInstances()
        {
            return QuizlrContext.Current.AnswerInstances.ToList();
        }

        public AnswerInstance GetInstance(int instanceId)
        {
            return QuizlrContext.Current.AnswerInstances.FirstOrDefault(o => o.AnswerInstanceId == instanceId);
        }

        public AnswerInstance CreateInstance(AnswerInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.AnswerInstances.Add(instance);
            ctx.SaveChanges();
            return instance;
        }

        public void UpdateInstance(AnswerInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.AnswerInstances.Attach(instance);
            ctx.Entry(instance).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteInstance(AnswerInstance instance)
        {
            var ctx = QuizlrContext.Current;
            ctx.AnswerInstances.Remove(instance);
            ctx.SaveChanges();
        }
    }
}