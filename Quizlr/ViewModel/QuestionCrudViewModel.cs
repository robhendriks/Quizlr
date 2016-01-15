using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Repository;

namespace Quizlr.ViewModel
{
    public class QuestionCrudViewModel : ViewModelBase
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;

        private bool _isNewAnswer, _isNewQuestion;

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
            Initialize();
        }

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        public RelayCommand SaveQuestionCommand { get; set; }
        public RelayCommand SaveAnswerCommand { get; set; }
        public RelayCommand AddQuestionCommand { get; set; }
        public RelayCommand AddAnswerCommand { get; set; }
        public RelayCommand DeleteQuestionCommand { get; set; }
        public RelayCommand DeleteAnswerCommand { get; set; }

        public bool IsNewQuestion
        {
            get { return _isNewQuestion; }
            private set
            {
                _isNewQuestion = value;
                RaisePropertyChanged(() => IsNewQuestion);
                Invalidate();
            }
        }

        public bool IsNewAnswer
        {
            get { return _isNewAnswer; }
            private set
            {
                _isNewAnswer = value;
                RaisePropertyChanged(() => IsNewAnswer);
                Invalidate();
            }
        }

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
                PopulateAnswers();
                Invalidate();
            }
        }

        public AnswerViewModel SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                RaisePropertyChanged(() => SelectedAnswer);
                Invalidate();
            }
        }

        private void Initialize()
        {
            SaveQuestionCommand = new RelayCommand(SaveQuestion, CanSaveQuestion);
            SaveAnswerCommand = new RelayCommand(SaveAnswer, CanSaveAnswer);
            AddQuestionCommand = new RelayCommand(AddQuestion, CanAddQuestion);
            AddAnswerCommand = new RelayCommand(AddAnswer, CanAddAnswer);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion, CanDeleteQuestion);
            DeleteAnswerCommand = new RelayCommand(DeleteAnswer, CanDeleteAnswer);

            Populate();
        }

        private void Invalidate()
        {
            // Question Commands
            SaveQuestionCommand.RaiseCanExecuteChanged();
            AddQuestionCommand.RaiseCanExecuteChanged();
            DeleteQuestionCommand.RaiseCanExecuteChanged();
            // Answer Commands
            SaveAnswerCommand.RaiseCanExecuteChanged();
            AddAnswerCommand.RaiseCanExecuteChanged();
            DeleteAnswerCommand.RaiseCanExecuteChanged();
        }

        private bool CanDeleteAnswer()
        {
            return !IsNewAnswer && !IsNewQuestion && SelectedAnswer != null && SelectedQuestion != null;
        }

        private bool CanDeleteQuestion()
        {
            return !IsNewQuestion && !IsNewAnswer && SelectedQuestion != null;
        }

        private bool CanAddAnswer()
        {
            return Answers?.Count < 4 && !IsNewAnswer && !IsNewQuestion && SelectedQuestion != null;
        }

        private bool CanAddQuestion()
        {
            return !IsNewQuestion && !IsNewAnswer;
        }

        private bool CanSaveAnswer()
        {
            return SelectedAnswer != null;
        }

        private bool CanSaveQuestion()
        {
            return SelectedQuestion != null && !IsNewAnswer;
        }

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

        private void SaveQuestion()
        {
            if (string.IsNullOrEmpty(SelectedQuestion.Text))
                MessageBox.Show("Vul a.u.b. een tekst in.");
            else if (SelectedQuestion.CategoryId == 0)
                MessageBox.Show("Selecteer a.u.b. een categorie.");
            else if (IsNewQuestion)
            {
                var poco = _questionRepository.CreateQuestion(SelectedQuestion);
                SelectedQuestion = new QuestionViewModel(poco);
                Questions.Add(SelectedQuestion);
                IsNewQuestion = false;
                MessageBox.Show("Vraag toegevoegd.");
            }
            else if (SelectedQuestion != null)
            {
                _questionRepository.UpdateQuestion(SelectedQuestion);
                MessageBox.Show("Vraag aagepast.");
            }
        }

        private void SaveAnswer()
        {
            if (string.IsNullOrEmpty(SelectedAnswer.Text))
                MessageBox.Show("Vul a.u.b. een tekst in.");
            else if (IsNewAnswer)
            {
                var poco = SelectedAnswer.Poco;
                poco.QuestionId = SelectedQuestion.QuestionId;
                poco = _answerRepository.CreateAnswer(SelectedAnswer);
                SelectedAnswer = new AnswerViewModel(poco);
                Answers.Add(SelectedAnswer);
                IsNewAnswer = false;
                MessageBox.Show("Antwoord toegevoegd.");
            }
            else if (SelectedAnswer != null)
            {
                _answerRepository.UpdateAnswer(SelectedAnswer);
                MessageBox.Show("Antwoord aagepast.");
            }
        }

        private void AddQuestion()
        {
            if (IsNewQuestion) return;
            IsNewQuestion = true;
            SelectedQuestion = new QuestionViewModel();
        }

        private void AddAnswer()
        {
            if (IsNewAnswer) return;
            IsNewAnswer = true;
            SelectedAnswer = new AnswerViewModel();
        }

        private void DeleteQuestion()
        {
            _questionRepository.DeleteQuestion(SelectedQuestion);
            Questions.Remove(SelectedQuestion);
            SelectedQuestion = null;
        }

        private void DeleteAnswer()
        {
            _answerRepository.DeleteAnswer(SelectedAnswer);
            Answers.Remove(SelectedAnswer);
            SelectedAnswer = null;
        }
    }
}