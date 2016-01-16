using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class AnswerInstanceViewModel : SimpleViewModel<AnswerInstance>
    {
        public AnswerInstanceViewModel(AnswerInstance poco = null) : base(poco)
        {
        }

        public int AnswerInstanceId
        {
            get { return Poco.AnswerInstanceId; }
            set
            {
                Poco.AnswerInstanceId = value;
                RaisePropertyChanged(() => AnswerInstanceId);
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

        public int QuestionInstanceId
        {
            get { return Poco.QuestionInstanceId; }
            set
            {
                Poco.QuestionInstanceId = value;
                RaisePropertyChanged(() => QuestionInstanceId);
            }
        }

        public virtual QuestionInstance QuestionInstance
        {
            get { return Poco.QuestionInstance; }
            set
            {
                Poco.QuestionInstance = value;
                RaisePropertyChanged(() => QuestionInstance);
            }
        }
    }
}