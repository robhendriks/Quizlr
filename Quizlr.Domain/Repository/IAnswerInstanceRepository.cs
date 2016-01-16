using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IAnswerInstanceRepository
    {
        List<AnswerInstance> GetInstances();
        AnswerInstance GetInstance(int instanceId);
        AnswerInstance CreateInstance(AnswerInstance instance);
        void UpdateInstance(AnswerInstance instance);
        void DeleteInstance(AnswerInstance instance);
    }
}