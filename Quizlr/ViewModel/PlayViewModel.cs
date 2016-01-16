using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

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
        private int _questionIndex, _questionCount;
        private QuestionInstance _questionInstance;
        private QuizInstance _quizInstance;

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

        public QuizInstance QuizInstance => _quizInstance;
        public QuestionInstance QuestionInstance => _questionInstance;

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
            StopCommand = new RelayCommand(Stop);
            NextCommand = new RelayCommand(Next);
        }

        private void Next()
        {
            // TODO
        }

        private void Stop()
        {
            // TODO
        }

        private void ResetQuiz()
        {
            if (CurrentQuiz == null)
                return;
            _quizInstance = _quizInstanceRepository.CreateInstance(CurrentQuiz.Poco);
            _enumerator = _currentQuiz.QuizQuestions.GetEnumerator();
            QuestionCount = _currentQuiz.QuizQuestionCount;
            QuestionIndex = 0;
            NextQuestion();
        }

        private void NextQuestion()
        {
            if (_enumerator.MoveNext())
            {
                QuestionIndex++;
                CurrentQuestion = new QuestionViewModel(_enumerator.Current.Question);
                _questionInstance = CurrentQuestion.Poco;
                _questionInstance.QuizInstanceId = _quizInstance.QuizInstanceId;
            }

            // TODO: end quiz
        }

        private void ResetQuestion()
        {
            if (CurrentQuestion == null)
                Answers = null;
            else
            {
                var answers = CurrentQuestion.Answers.Select(a => new AnswerViewModel(a));
                Answers = new ObservableCollection<AnswerViewModel>(answers);
            }
            RaisePropertyChanged(() => Answers);
        }
    }
}