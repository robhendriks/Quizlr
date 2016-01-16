using System;
using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class QuestionInstanceViewModel : SimpleViewModel<QuestionInstance>
    {
        public QuestionInstanceViewModel(QuestionInstance poco = null) : base(poco)
        {
        }

        public int QuestionInstanceId
        {
            get { return Poco.QuestionInstanceId; }
            set
            {
                Poco.QuestionInstanceId = value;
                RaisePropertyChanged(() => QuestionInstanceId);
            }
        }

        public string Text
        {
            get { return Poco.Text; }
            set
            {
                Poco.Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public string Category
        {
            get { return Poco.Category; }
            set
            {
                Poco.Category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        public string Value
        {
            get { return Poco.Value; }
            set
            {
                Poco.Value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsCorrect
        {
            get { return Poco.IsCorrect; }
            set
            {
                Poco.IsCorrect = value;
                RaisePropertyChanged(() => IsCorrect);
            }
        }

        public DateTime Created
        {
            get { return Poco.Created; }
            set
            {
                Poco.Created = value;
                RaisePropertyChanged(() => Created);
            }
        }

        public DateTime? Completed
        {
            get { return Poco.Completed; }
            set
            {
                Poco.Completed = value;
                RaisePropertyChanged(() => Completed);
            }
        }

        public int QuizInstanceId
        {
            get { return Poco.QuizInstanceId; }
            set
            {
                Poco.QuizInstanceId = value;
                RaisePropertyChanged(() => QuizInstanceId);
            }
        }

        public virtual QuizInstance QuizInstance
        {
            get { return Poco.QuizInstance; }
            set
            {
                Poco.QuizInstance = value;
                RaisePropertyChanged(() => QuizInstanceId);
            }
        }

        public virtual ICollection<AnswerInstance> AnswerInstances
        {
            get { return Poco.AnswerInstances; }
            set
            {
                Poco.AnswerInstances = value;
                RaisePropertyChanged(() => AnswerInstances);
            }
        }
    }
}