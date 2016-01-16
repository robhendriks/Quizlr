namespace Quizlr.Domain.Model
{
    public class AnswerInstance
    {
        public int AnswerInstanceId { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionInstanceId { get; set; }

        public virtual QuestionInstance QuestionInstance { get; set; }
    }
}