using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class QuestionViewModel : SimpleViewModel<Question>
    {
        public QuestionViewModel(Question poco = null) : base(poco)
        {
        }

        public int QuestionId
        {
            get { return Poco.QuestionId; }
            set
            {
                Poco.QuestionId = value;
                RaisePropertyChanged(() => QuestionId);
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

        public int CategoryId
        {
            get { return Poco.CategoryId; }
            set
            {
                Poco.CategoryId = value;
                RaisePropertyChanged(() => CategoryId);
            }
        }

        public Category Category
        {
            get { return Poco.Category; }
            set
            {
                Poco.Category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        public ICollection<Answer> Answers
        {
            get { return Poco.Answers; }
            set
            {
                Poco.Answers = value;
                RaisePropertyChanged(() => Answers);
            }
        }

        public int AnswerCount => Answers?.Count ?? 0;

        public ICollection<QuizQuestion> QuizQuestions
        {
            get { return Poco.QuizQuestions; }
            set
            {
                Poco.QuizQuestions = value;
                RaisePropertyChanged(() => QuizQuestions);
            }
        }
    }
}