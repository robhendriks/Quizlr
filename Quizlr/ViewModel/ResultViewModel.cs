using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Quizlr.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        private QuizInstanceViewModel _quizInstance;

        public ResultViewModel()
        {
            Populate();
        }

        public QuizInstanceViewModel QuizInstance
        {
            get { return _quizInstance; }
            set
            {
                _quizInstance = value;
                RaisePropertyChanged(() => QuizInstance);
                Populate();
            }
        }

        public ObservableCollection<QuestionInstanceViewModel> QuestionInstances { get; set; }

        private void Populate()
        {
            if (QuizInstance == null)
                QuestionInstances = null;
            else
            {
                var questionInstances = _quizInstance.QuestionInstances
                    .Where(o => o.Completed != null)
                    .OrderBy(o => o.Completed)
                    .Select(qi => new QuestionInstanceViewModel(qi));
                QuestionInstances = new ObservableCollection<QuestionInstanceViewModel>(questionInstances);
            }
            RaisePropertyChanged(() => QuestionInstances);
        }
    }
}