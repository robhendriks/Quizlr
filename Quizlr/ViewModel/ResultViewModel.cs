using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Quizlr.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        private int _correctCount, _incorrectCount, _score;
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

        public int CorrectCount
        {
            get { return _correctCount; }
            set
            {
                _correctCount = value;
                RaisePropertyChanged(() => CorrectCount);
            }
        }

        public int IncorrectCount
        {
            get { return _incorrectCount; }
            set
            {
                _incorrectCount = value;
                RaisePropertyChanged(() => IncorrectCount);
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged(() => Score);
            }
        }

        private void Populate()
        {
            if (QuizInstance == null)
            {
                QuestionInstances = null;
                CorrectCount = IncorrectCount = 0;
            }
            else
            {
                var questionInstances = _quizInstance.QuestionInstances
                    .Where(o => o.Completed != null)
                    .OrderBy(o => o.Completed)
                    .Select(qi => new QuestionInstanceViewModel(qi));
                QuestionInstances = new ObservableCollection<QuestionInstanceViewModel>(questionInstances);

                var max = QuestionInstances.Count;
                CorrectCount = QuestionInstances.Count(qi => qi.IsCorrect);
                IncorrectCount = max - _correctCount;
                Score = (int) Math.Ceiling(((double)CorrectCount/max)*100);
            }
            RaisePropertyChanged(() => QuestionInstances);
        }
    }
}