using System.Collections.Generic;

namespace Quizlr.Domain.Model
{
    public class QuestionInstance
    {
        public QuestionInstance()
        {
            AnswerInstances = new HashSet<AnswerInstance>();
        }

        public int QuestionInstanceId { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public string Value { get; set; }

        public int QuizInstanceId { get; set; }

        public virtual QuizInstance QuizInstance { get; set; }

        public virtual ICollection<AnswerInstance> AnswerInstances { get; set; }
    }
}