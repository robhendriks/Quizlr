using System;
using System.Collections.Generic;

namespace Quizlr.Domain.Model
{
    public class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuizQuestions = new HashSet<QuizQuestion>();
        }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }

        public static implicit operator QuestionInstance(Question question)
        {
            return new QuestionInstance
            {
                Text = question.Text,
                Category = question.Category.Name,
                Created = DateTime.Now
            };
        }
    }
}