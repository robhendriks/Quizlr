using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IQuestionRepository
    {
        List<Question> GetQuestions();
        Question GetQuestion(int questionId);
        Question CreateQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(Question question);
    }
}