using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Repository;
using Quizlr.Lib.Utility;
using Quizlr.View;

namespace Quizlr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuizRepository _quizRepository;

        private QuizViewModel _selectedQuiz;

        public HomeViewModel(IQuizRepository quizRepository, ICategoryRepository categoryRepository)
        {
            if (quizRepository == null)
                throw new ArgumentNullException(nameof(quizRepository));
            if (categoryRepository == null)
                throw new ArgumentNullException(nameof(categoryRepository));
            _quizRepository = quizRepository;
            _categoryRepository = categoryRepository;
            Initialize();
        }

        public ObservableCollection<QuizViewModel> Quizzes { get; set; }
        public ObservableCollection<CategoryViewModel> Categories { get; set; }

        public RelayCommand OpenQuizzesCommand { get; set; }
        public RelayCommand OpenQuestionsCommand { get; set; }
        public RelayCommand OpenResultsCommand { get; set; }

        public QuizViewModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                RaisePropertyChanged(() => SelectedQuiz);
            }
        }

        private void Initialize()
        {
            OpenQuizzesCommand = new RelayCommand(OpenQuizzes);
            OpenQuestionsCommand = new RelayCommand(OpenQuestions);
            OpenResultsCommand = new RelayCommand(OpenResults);
            Populate();
        }

        private void Play(QuizViewModel quiz)
        {
            if (quiz == null)
                MessageBox.Show("Onverwachte fout.");
            var vm = NinjectServiceLocator.GetInstance<PlayViewModel>();
            vm.CurrentQuiz = quiz;
            var wnd = WindowHelper.Switch<PlayWindow>();
            wnd.Closed += (sender, args) => WindowHelper.Show<HomeWindow>();
        }

        private void OpenResults()
        {
        }

        private void OpenQuizzes()
        {
            var wnd = WindowHelper.Switch<QuestionCrudWindow, QuizCrudWindow>();
            wnd.Closed += (sender, args) => Populate();
        }

        private void OpenQuestions()
        {
            var wnd = WindowHelper.Switch<QuizCrudWindow, QuestionCrudWindow>();
            wnd.Closed += (sender, args) => Populate();
        }

        private void Populate()
        {
            var quizzes = _quizRepository.GetQuizzes()
                .Select(q => new QuizViewModel(q)
                {
                    PlayCommand = new RelayCommand<QuizViewModel>(Play)
                })
                .Where(q => q.QuizQuestionCount > 1);
            Quizzes = new ObservableCollection<QuizViewModel>(quizzes);
            RaisePropertyChanged(() => Quizzes);

            var categories = _categoryRepository.GetCategories().Select(c => new CategoryViewModel(c));
            Categories = new ObservableCollection<CategoryViewModel>(categories);
            RaisePropertyChanged(() => Categories);
        }
    }
}