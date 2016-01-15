using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Quizlr.Domain.Repository;

namespace Quizlr.ViewModel
{
    public class QuestionCrudViewModel : ViewModelBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionCrudViewModel(IQuestionRepository questionRepository)
        {
            if (questionRepository == null)
                throw new ArgumentNullException(nameof(questionRepository));
            _questionRepository = questionRepository;
            Populate();
        }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        private void Populate()
        {
            var questions = _questionRepository.GetQuestions().Select(q => new QuestionViewModel(q));
            Questions = new ObservableCollection<QuestionViewModel>(questions);
        }
    }
}