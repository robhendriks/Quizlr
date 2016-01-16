using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IQuestionInstanceRepository
    {
        List<QuestionInstance> GetInstances();
        QuestionInstance GetInstance(int instanceId);
        QuestionInstance CreateInstance(QuestionInstance instance);
        void UpdateInstance(QuestionInstance instance);
        void DeleteInstance(QuestionInstance instance);
    }
}