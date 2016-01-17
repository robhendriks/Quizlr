using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Test.Utility
{
    public static class QuizGenerator
    {
        public static Quiz Generate()
        {
            var category = new Category {CategoryId = 1, Name = "FooBar"};
            var quiz = new Quiz {QuizId = 1, Name = "FooBar"};
            var questions = new List<Question>
            {
                new Question
                {
                    QuestionId = 1,
                    Text = "Foo",
                    Category = category,
                    Answers = new List<Answer>
                    {
                        new Answer {AnswerId = 1, Text = "Foo", IsCorrect = true},
                        new Answer {AnswerId = 2, Text = "Bar", IsCorrect = false}
                    }
                },
                new Question
                {
                    QuestionId = 2,
                    Text = "Bar",
                    Category = category,
                    Answers = new List<Answer>
                    {
                        new Answer {AnswerId = 3, Text = "Foo", IsCorrect = true},
                        new Answer {AnswerId = 4, Text = "Bar", IsCorrect = false}
                    }
                }
            };
            var quizQuestions = new List<QuizQuestion>();
            foreach (var question in questions)
            {
                var quizQuestion = new QuizQuestion
                {
                    QuizId = quiz.QuizId,
                    Quiz = quiz,
                    QuestionId = question.QuestionId,
                    Question = question
                };
                question.QuizQuestions = quizQuestions;
                quizQuestions.Add(quizQuestion);
            }
            quiz.QuizQuestions = quizQuestions;
            return quiz;
        }
    }
}