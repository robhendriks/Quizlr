using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.ViewModel
{
    public class QuizCrudViewModel : ViewModelBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly IQuizRepository _quizRepository;

        private bool _isNewQuiz;
        private QuestionViewModel _selectedIncludedQuestion, _selectedExcludedQuestion;

        private QuizViewModel _selectedQuiz;

        public QuizCrudViewModel(IQuizRepository quizRepository, IQuestionRepository questionRepository,
            IQuizQuestionRepository quizQuestionRepository)
        {
            if (quizRepository == null)
                throw new ArgumentNullException(nameof(quizRepository));
            if (questionRepository == null)
                throw new ArgumentNullException(nameof(questionRepository));
            if (quizQuestionRepository == null)
                throw new ArgumentNullException(nameof(quizQuestionRepository));
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
            _quizQuestionRepository = quizQuestionRepository;
            Initialize();
        }

        public ObservableCollection<QuizViewModel> Quizzes { get; set; }
        public ObservableCollection<QuestionViewModel> IncludedQuestions { get; set; }
        public ObservableCollection<QuestionViewModel> ExcludedQuestions { get; set; }

        public RelayCommand SaveQuizCommand { get; set; }
        public RelayCommand AddQuizCommand { get; set; }
        public RelayCommand DeleteQuizCommand { get; set; }
        public RelayCommand IncludeQuestionCommand { get; set; }
        public RelayCommand ExcludeQuestionCommand { get; set; }

        public bool IsNewQuiz
        {
            get { return _isNewQuiz; }
            set
            {
                _isNewQuiz = value;
                RaisePropertyChanged(() => IsNewQuiz);
                Invalidate();
            }
        }

        public QuizViewModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                RaisePropertyChanged(() => SelectedQuiz);
                PopulateQuestions();
                Invalidate();
            }
        }

        public QuestionViewModel SelectedIncludedQuestion
        {
            get { return _selectedIncludedQuestion; }
            set
            {
                _selectedIncludedQuestion = value;
                RaisePropertyChanged(() => SelectedIncludedQuestion);
                Invalidate();
            }
        }

        public QuestionViewModel SelectedExcludedQuestion
        {
            get { return _selectedExcludedQuestion; }
            set
            {
                _selectedExcludedQuestion = value;
                RaisePropertyChanged(() => SelectedExcludedQuestion);
                Invalidate();
            }
        }

        private void Initialize()
        {
            SaveQuizCommand = new RelayCommand(SaveQuiz, CanSaveQuiz);
            AddQuizCommand = new RelayCommand(AddQuiz, CanAddQuiz);
            DeleteQuizCommand = new RelayCommand(DeleteQuiz, CanDeleteQuiz);
            IncludeQuestionCommand = new RelayCommand(IncludeQuestion, CanIncludeQuestion);
            ExcludeQuestionCommand = new RelayCommand(ExcludeQuestion, CanExcludeQuestion);

            Populate();
        }

        private void Invalidate()
        {
            SaveQuizCommand.RaiseCanExecuteChanged();
            AddQuizCommand.RaiseCanExecuteChanged();
            DeleteQuizCommand.RaiseCanExecuteChanged();
            IncludeQuestionCommand.RaiseCanExecuteChanged();
            ExcludeQuestionCommand.RaiseCanExecuteChanged();
        }

        private bool CanExcludeQuestion()
        {
            return !IsNewQuiz && SelectedQuiz != null && SelectedIncludedQuestion != null;
        }

        private void ExcludeQuestion()
        {
            var vm = SelectedIncludedQuestion;
            IncludedQuestions.Remove(vm);
            ExcludedQuestions.Add(vm);
            var qq = _quizQuestionRepository.GetByQuizIdAndQuestionId(SelectedQuiz.QuizId, vm.QuestionId);
            if (qq == null)
            {
                MessageBox.Show("Onverwachte fout.");
                return;
            }
            SelectedQuiz.QuizQuestions.Remove(qq);
            _quizQuestionRepository.DeleteQuizQuestion(qq);
        }

        private bool CanIncludeQuestion()
        {
            return !IsNewQuiz && SelectedQuiz != null && SelectedExcludedQuestion != null;
        }

        private void IncludeQuestion()
        {
            var tmp = SelectedExcludedQuestion;
            ExcludedQuestions.Remove(tmp);
            IncludedQuestions.Add(tmp);
            var qq = new QuizQuestion
            {
                QuizId = SelectedQuiz.QuizId,
                QuestionId = tmp.QuestionId
            };
            _quizQuestionRepository.CreateQuizQuestion(qq);
            SelectedQuiz.QuizQuestions.Add(qq);
        }

        private bool CanDeleteQuiz()
        {
            return !IsNewQuiz && SelectedQuiz != null;
        }

        private void DeleteQuiz()
        {
            _quizRepository.DeleteQuiz(SelectedQuiz);
            Quizzes.Remove(SelectedQuiz);
            SelectedQuiz = null;
        }

        private bool CanAddQuiz()
        {
            return !IsNewQuiz;
        }

        private void AddQuiz()
        {
            if (IsNewQuiz) return;
            IsNewQuiz = true;
            SelectedQuiz = new QuizViewModel();
        }

        private bool CanSaveQuiz()
        {
            return SelectedQuiz != null;
        }

        private void SaveQuiz()
        {
            if (string.IsNullOrEmpty(SelectedQuiz.Name))
                MessageBox.Show("Vul a.u.b. een naam in.");
            else if (IsNewQuiz)
            {
                var poco = _quizRepository.CreateQuiz(SelectedQuiz);
                SelectedQuiz = new QuizViewModel(poco);
                Quizzes.Add(SelectedQuiz);
                IsNewQuiz = false;
                MessageBox.Show("Quiz toegevoegd.");
            }
            else if (SelectedQuiz != null)
            {
                _quizRepository.UpdateQuiz(SelectedQuiz);
                MessageBox.Show("Quiz aagepast.");
            }
        }

        private void Populate()
        {
            var quizzes = _quizRepository.GetQuizzes().Select(q => new QuizViewModel(q));
            Quizzes = new ObservableCollection<QuizViewModel>(quizzes);
        }

        private void PopulateQuestions()
        {
            if (SelectedQuiz == null)
                IncludedQuestions = ExcludedQuestions = null;
            else if (SelectedQuiz.QuizQuestions != null)
            {
                var incl = _quizQuestionRepository.GetQuizQuestions()
                    .Where(q => q.QuizId == SelectedQuiz.QuizId)
                    .Select(q => new QuestionViewModel(q.Question));
                IncludedQuestions = new ObservableCollection<QuestionViewModel>(incl);

                var excl = from q1 in _questionRepository.GetQuestions()
                    where !(from q2 in incl
                        select q2.QuestionId)
                        .Contains(q1.QuestionId)
                    select new QuestionViewModel(q1);
                ExcludedQuestions = new ObservableCollection<QuestionViewModel>(excl);
            }
            RaisePropertyChanged(() => IncludedQuestions);
            RaisePropertyChanged(() => ExcludedQuestions);
        }
    }
}