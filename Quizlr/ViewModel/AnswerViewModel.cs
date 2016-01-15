using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class AnswerViewModel : SimpleViewModel<Answer>
    {
        public AnswerViewModel(Answer poco = null) : base(poco)
        {
        }

        public int AnswerId
        {
            get { return Poco.AnswerId; }
            set
            {
                Poco.AnswerId = value;
                RaisePropertyChanged(() => AnswerId);
            }
        }

        public string Text
        {
            get { return Poco.Text; }
            set
            {
                Poco.Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public bool IsCorrect
        {
            get { return Poco.IsCorrect; }
            set
            {
                Poco.IsCorrect = value;
                RaisePropertyChanged(() => IsCorrect);
            }
        }

        public int QuestionId
        {
            get { return Poco.QuestionId; }
            set
            {
                Poco.QuestionId = value;
                RaisePropertyChanged(() => QuestionId);
            }
        }

        public Question Question
        {
            get { return Poco.Question; }
            set
            {
                Poco.Question = value;
                RaisePropertyChanged(() => QuestionId);
            }
        }
    }
}