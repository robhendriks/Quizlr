using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IQuizRepository
    {
        List<Quiz> GetQuizzes();
        Quiz GetQuiz(int quizId);
        Quiz CreateQuiz(Quiz quiz);
        void UpdateQuiz(Quiz quiz);
        void DeleteQuiz(Quiz quiz);
    }
}