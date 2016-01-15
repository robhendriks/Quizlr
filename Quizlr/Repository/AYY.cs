using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbQuizRepository : IQuizRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Quiz> GetQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public Quiz GetQuiz(int quizId)
        {
            return _context.Quizzes.FirstOrDefault(o => o.QuizId == quizId);
        }

        public Quiz CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _context.Quizzes.Attach(quiz);
            _context.Entry(quiz).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuiz(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
        }
    }

    public class DbCategoryRepository : ICategoryRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(o => o.CategoryId == categoryId);
        }

        public Category CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }

    public class DbQuestionRepository : IQuestionRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public Question GetQuestion(int questionId)
        {
            return _context.Questions.FirstOrDefault(o => o.QuestionId == questionId);
        }

        public Question CreateQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }

    public class DbQuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<QuizQuestion> GetQuizQuestions()
        {
            return _context.QuizQuestions.ToList();
        }

        public QuizQuestion GetQuizQuestion(int quizQuestionId)
        {
            return _context.QuizQuestions.FirstOrDefault(o => o.QuizQuestionId == quizQuestionId);
        }

        public QuizQuestion CreateQuizQuestion(QuizQuestion quizQuestion)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuizQuestion(QuizQuestion quizQuestion)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuizQuestion(QuizQuestion quizQuestion)
        {
            throw new NotImplementedException();
        }
    }

    public class DbAnswerRepository : IAnswerRepository
    {
        private readonly QuizlrContext _context = new QuizlrContext();

        public List<Answer> GetAnswers()
        {
            return _context.Answers.ToList();
        }

        public Answer GetAnswer(int answerId)
        {
            return _context.Answers.FirstOrDefault(o => o.AnswerId == answerId);
        }

        public Answer CreateAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}