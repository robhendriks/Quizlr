using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Quizlr.Domain.Repository;

namespace Quizlr.ViewModel
{
    public class QuestionCrudViewModel : ViewModelBase
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;

        private AnswerViewModel _selectedAnswer;
        private QuestionViewModel _selectedQuestion;

        public QuestionCrudViewModel(IAnswerRepository answerRepository, ICategoryRepository categoryRepository,
            IQuestionRepository questionRepository)
        {
            if (answerRepository == null)
                throw new ArgumentNullException(nameof(answerRepository));
            if (categoryRepository == null)
                throw new ArgumentNullException(nameof(categoryRepository));
            if (questionRepository == null)
                throw new ArgumentNullException(nameof(questionRepository));
            _answerRepository = answerRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            Populate();
        }

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
                RaisePropertyChanged(() => HasQuestionSelection);
                PopulateAnswers();
            }
        }

        public AnswerViewModel SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                RaisePropertyChanged(() => SelectedAnswer);
                RaisePropertyChanged(() => HasAnswerSelection);
            }
        }

        public bool HasQuestionSelection => SelectedQuestion != null;
        public bool HasAnswerSelection => SelectedAnswer != null;

        private void Populate()
        {
            var categories = _categoryRepository.GetCategories().Select(c => new CategoryViewModel(c));
            Categories = new ObservableCollection<CategoryViewModel>(categories);

            var questions = _questionRepository.GetQuestions().Select(q => new QuestionViewModel(q));
            Questions = new ObservableCollection<QuestionViewModel>(questions);
        }

        private void PopulateAnswers()
        {
            if (SelectedQuestion == null)
                Answers = null;
            else
            {
                var answers = _answerRepository.GetAnswers()
                    .Where(a => a.QuestionId == SelectedQuestion.QuestionId)
                    .Select(a => new AnswerViewModel(a));
                Answers = new ObservableCollection<AnswerViewModel>(answers);
            }
            RaisePropertyChanged(() => Answers);
        }
    }
}