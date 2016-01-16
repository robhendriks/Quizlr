using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;
using Quizlr.Lib.Utility;
using Quizlr.View;

namespace Quizlr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizRepository _quizRepository;

        private CategoryViewModel _selectedCategory;

        public HomeViewModel(IQuizRepository quizRepository, ICategoryRepository categoryRepository,
            IQuestionRepository questionRepository)
        {
            if (quizRepository == null)
                throw new ArgumentNullException(nameof(quizRepository));
            if (categoryRepository == null)
                throw new ArgumentNullException(nameof(categoryRepository));
            if (questionRepository == null)
                throw new ArgumentNullException(nameof(questionRepository));
            _quizRepository = quizRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            Initialize();
        }

        public ObservableCollection<QuizViewModel> Quizzes { get; set; }
        public ObservableCollection<CategoryViewModel> Categories { get; set; }

        public RelayCommand OpenQuizzesCommand { get; set; }
        public RelayCommand OpenQuestionsCommand { get; set; }
        public RelayCommand OpenResultsCommand { get; set; }
        public RelayCommand PlayRandomCommand { get; set; }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
                Invalidate();
            }
        }

        private void Initialize()
        {
            OpenQuizzesCommand = new RelayCommand(OpenQuizzes);
            OpenQuestionsCommand = new RelayCommand(OpenQuestions);
            OpenResultsCommand = new RelayCommand(OpenResults);
            PlayRandomCommand = new RelayCommand(PlayRandom, CanPlayRandom);
            Populate();
        }

        private void Invalidate()
        {
            PlayRandomCommand.RaiseCanExecuteChanged();
        }

        private bool CanPlayRandom()
        {
            return SelectedCategory != null;
        }

        private void PlayRandom()
        {
            if (SelectedCategory == null) return;
            var quiz = new QuizViewModel
            {
                QuizId = 1337,
                Name = SelectedCategory.Name
            };
            var questions = _questionRepository.GetQuestions()
                .Where(q => q.CategoryId == SelectedCategory.CategoryId)
                .Select(qq => new QuizQuestion
                {
                    QuizId = quiz.QuizId,
                    Quiz = quiz,
                    QuestionId = qq.QuestionId,
                    Question = qq
                })
                .ToList();
            if (questions.Count < 2)
            {
                MessageBox.Show("De opgegeven categorie bevat niet genoeg vragen.");
                return;
            }
            quiz.QuizQuestions = questions;
            Play(quiz);
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
            WindowHelper.Show<ResultsWindow>();
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

            Invalidate();
        }
    }
}