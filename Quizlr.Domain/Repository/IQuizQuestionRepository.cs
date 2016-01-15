using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface IQuizQuestionRepository
    {
        List<QuizQuestion> GetQuizQuestions();
        QuizQuestion GetQuizQuestion(int quizQuestionId);
        QuizQuestion CreateQuizQuestion(QuizQuestion quizQuestion);
        void UpdateQuizQuestion(QuizQuestion quizQuestion);
        void DeleteQuizQuestion(QuizQuestion quizQuestion);
    }
}