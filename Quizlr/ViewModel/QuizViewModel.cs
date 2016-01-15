using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class QuizViewModel : SimpleViewModel<Quiz>
    {
        public QuizViewModel(Quiz poco = null) : base(poco)
        {
        }

        public int QuizId
        {
            get { return Poco.QuizId; }
            set
            {
                Poco.QuizId = value;
                RaisePropertyChanged(() => QuizId);
            }
        }

        public string Name
        {
            get { return Poco.Name; }
            set
            {
                Poco.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}