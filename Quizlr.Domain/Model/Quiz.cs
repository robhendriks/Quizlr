using System.Collections.Generic;

namespace Quizlr.Domain.Model
{
    public class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new HashSet<QuizQuestion>();
        }

        public int QuizId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}