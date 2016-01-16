using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IQuizInstanceRepository
    {
        List<QuizInstance> GetInstances();
        QuizInstance GetInstance(int instanceId);
        QuizInstance CreateInstance(QuizInstance instance);
        void UpdateInstance(QuizInstance instance);
        void DeleteInstance(QuizInstance instance);
    }
}