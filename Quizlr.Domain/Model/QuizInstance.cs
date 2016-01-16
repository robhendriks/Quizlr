using System.Collections.Generic;

namespace Quizlr.Domain.Model
{
    public class QuizInstance
    {
        public QuizInstance()
        {
            QuestionInstances = new HashSet<QuestionInstance>();
        }

        public int QuizInstanceId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<QuestionInstance> QuestionInstances { get; set; }
    }
}