using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;
using Quizlr.Lib.Utility;
using Quizlr.View;

namespace Quizlr.ViewModel
{
    public class PlayViewModel : ViewModelBase
    {
        private readonly IAnswerInstanceRepository _answerInstanceRepository;
        private readonly IQuestionInstanceRepository _questionInstanceRepository;
        private readonly IQuizInstanceRepository _quizInstanceRepository;
        private QuestionViewModel _currentQuestion;
        private QuizViewModel _currentQuiz;
        private IEnumerator<QuizQuestion> _enumerator;
        private bool _hasAnswers, _isComplete;
        private int _questionIndex, _questionCount;
        private QuestionInstance _questionInstance;
        private QuizInstance _quizInstance;
        private AnswerViewModel _selectedAnswer;

        public PlayViewModel(IQuizInstanceRepository quizInstanceRepository,
            IQuestionInstanceRepository questionInstanceRepository, IAnswerInstanceRepository answerInstanceRepository)
        {
            if (quizInstanceRepository == null)
                throw new ArgumentNullException(nameof(quizInstanceRepository));
            if (questionInstanceRepository == null)
                throw new ArgumentNullException(nameof(questionInstanceRepository));
            if (answerInstanceRepository == null)
                throw new ArgumentNullException(nameof(answerInstanceRepository));
            _quizInstanceRepository = quizInstanceRepository;
            _questionInstanceRepository = questionInstanceRepository;
            _answerInstanceRepository = answerInstanceRepository;

            Initialize();
        }

        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        public RelayCommand StopCommand { get; set; }
        public RelayCommand NextCommand { get; set; }

        public QuizViewModel CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                if (value.QuizQuestionCount < 2)
                    throw new ArgumentException("Quiz must have at least 2 questions.", nameof(value));
                _currentQuiz = value;
                ResetQuiz();
                RaisePropertyChanged(() => CurrentQuiz);
            }
        }

        public QuestionViewModel CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                ResetQuestion();
                RaisePropertyChanged(() => CurrentQuestion);
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

        public QuizInstance QuizInstance => _quizInstance;
        public QuestionInstance QuestionInstance => _questionInstance;
        public bool IsComplete => _isComplete;

        public int QuestionIndex
        {
            get { return _questionIndex; }
            set
            {
                _questionIndex = value;
                RaisePropertyChanged(() => QuestionIndex);
            }
        }

        public int QuestionCount
        {
            get { return _questionCount; }
            set
            {
                _questionCount = value;
                RaisePropertyChanged(() => QuestionCount);
            }
        }

        private void Initialize()
        {
            StopCommand = new RelayCommand(Stop, CanStop);
            NextCommand = new RelayCommand(Next, CanNext);

            Invalidate();
        }

        private void Invalidate()
        {
            StopCommand.RaiseCanExecuteChanged();
            NextCommand.RaiseCanExecuteChanged();
        }

        private void Select(AnswerViewModel answer)
        {
            var selection = Answers.Where(a => a.IsSelected);
            foreach (var selected in selection)
                selected.IsSelected = false;
            answer.IsSelected = !answer.IsSelected;
            SelectedAnswer = answer;
        }

        private bool CanNext()
        {
            return !_isComplete && SelectedAnswer != null;
        }

        private void Next()
        {
            NextQuestion();
        }

        private bool CanStop()
        {
            return !_isComplete;
        }

        private void Stop()
        {
            StopQuiz();
        }

        private void ResetStats()
        {
            _isComplete = _hasAnswers = false;
            _questionCount = _questionIndex = 0;
        }

        private void ResetQuiz()
        {
            ResetStats();
            if (CurrentQuiz == null)
            {
                Invalidate();
                return;
            }
            _quizInstance = _quizInstanceRepository.CreateInstance(CurrentQuiz.Poco);
            _questionInstance = null;
            _enumerator = _currentQuiz.QuizQuestions.GetEnumerator();
            QuestionCount = _currentQuiz.QuizQuestionCount;
            QuestionIndex = 0;
            NextQuestion();
            Invalidate();
        }

        private void StopQuiz()
        {
            if (_isComplete) return;
            _isComplete = true;
            Invalidate();

            if (!_hasAnswers)
            {
                WindowHelper.Switch<HomeWindow>();
                return;
            }

            _quizInstance.Completed = DateTime.Now;
            _quizInstanceRepository.UpdateInstance(_quizInstance);

            // Set as current result
            var rvm = NinjectServiceLocator.GetInstance<ResultViewModel>();
            rvm.QuizInstance = new QuizInstanceViewModel(_quizInstance);

            // Add to results
            var rsvm = NinjectServiceLocator.GetInstance<ResultsViewModel>();
            rsvm.QuizInstances.Add(new QuizInstanceViewModel(_quizInstance));

            var wnd = WindowHelper.Switch<ResultWindow>();
            wnd.Closed += (sender, args) => WindowHelper.Show<HomeWindow>();
        }

        private void NextQuestion()
        {
            SaveQuestion();
            if (_enumerator.MoveNext())
            {
                QuestionIndex++;
                CurrentQuestion = new QuestionViewModel(_enumerator.Current.Question);
                _questionInstance = CurrentQuestion.Poco;
                _questionInstance.QuizInstanceId = _quizInstance.QuizInstanceId;
                SelectedAnswer = null;
                return;
            }
            StopQuiz();
        }

        private void SaveQuestion()
        {
            if (_isComplete || _questionInstance == null)
                return;
            if(SelectedAnswer == null)
                throw new InvalidOperationException("This should not happen.");
            _questionInstance.Value = SelectedAnswer.Text;
            _questionInstance.IsCorrect = SelectedAnswer.IsCorrect;
            _questionInstance.Completed = DateTime.Now;
            _questionInstance = _questionInstanceRepository.CreateInstance(_questionInstance);
            foreach (var answer in Answers)
            {
                var instance = (AnswerInstance) answer.Poco;
                instance.QuestionInstanceId = _questionInstance.QuestionInstanceId;
                _answerInstanceRepository.CreateInstance(instance);
            }
            _hasAnswers = true;
        }

        private void ResetQuestion()
        {
            if (CurrentQuestion == null)
                Answers = null;
            else
            {
                var answers = CurrentQuestion.Answers.Select(a => new AnswerViewModel(a)
                {
                    SelectCommand = new RelayCommand<AnswerViewModel>(Select)
                });
                Answers = new ObservableCollection<AnswerViewModel>(answers);
            }
            RaisePropertyChanged(() => Answers);
        }
    }
}