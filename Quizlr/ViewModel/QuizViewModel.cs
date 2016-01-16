using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class QuizViewModel : SimpleViewModel<Quiz>
    {
        public QuizViewModel(Quiz poco = null) : base(poco)
        {
        }

        public RelayCommand<QuizViewModel> PlayCommand { get; set; }

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

        public ICollection<QuizQuestion> QuizQuestions
        {
            get { return Poco.QuizQuestions; }
            set
            {
                Poco.QuizQuestions = value;
                RaisePropertyChanged(() => QuizQuestions);
            }
        }

        public int QuizQuestionCount => QuizQuestions?.Count ?? 0;
    }
}