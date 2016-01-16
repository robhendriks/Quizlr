using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class QuizInstanceViewModel : SimpleViewModel<QuizInstance>
    {
        public QuizInstanceViewModel(QuizInstance poco = null) : base(poco)
        {
        }

        public int QuizInstanceId
        {
            get { return Poco.QuizInstanceId; }
            set
            {
                Poco.QuizInstanceId = value;
                RaisePropertyChanged(() => QuizInstanceId);
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

        public virtual ICollection<QuestionInstance> QuestionInstances
        {
            get { return Poco.QuestionInstances; }
            set
            {
                Poco.QuestionInstances = value;
                RaisePropertyChanged(() => QuestionInstances);
            }
        }
    }
}