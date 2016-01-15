using System.Collections.Generic;

namespace Quizlr.Domain.Model
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}