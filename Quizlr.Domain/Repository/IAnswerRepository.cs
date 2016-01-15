using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IAnswerRepository
    {
        List<Answer> GetAnswers();
        Answer GetAnswer(int answerId);
        Answer CreateAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeleteAnswer(Answer answer);
    }
}
