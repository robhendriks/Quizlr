using Quizlr.Utility;

namespace Quizlr.ViewModel
{
    public class NinjectViewModelLocator : IViewModelLocator
    {
        public HomeViewModel Home
        {
            get { return Get<HomeViewModel>(); }
        }

        public PlayViewModel Play
        {
            get { return Get<PlayViewModel>(); }
        }

        public ResultViewModel Result
        {
            get { return Get<ResultViewModel>(); }
        }

        public ResultsViewModel Results
        {
            get { return Get<ResultsViewModel>(); }
        }

        public QuizCrudViewModel QuizCrud
        {
            get { return Get<QuizCrudViewModel>(); }
        }

        public QuestionCrudViewModel QuestionCrud
        {
            get { return Get<QuestionCrudViewModel>(); }
        }

        public T Get<T>() where T : class
        {
            return NinjectServiceLocator.GetInstance<T>();
        }
    }
}