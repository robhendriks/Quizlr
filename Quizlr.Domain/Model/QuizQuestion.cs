namespace Quizlr.Domain.Model
{
    public class QuizQuestion
    {
        public int QuizQuestionId { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}