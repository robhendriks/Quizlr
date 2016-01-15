using Ninject;
using Quizlr.Repository;

namespace Quizlr.ViewModel
{
    public class NinjectServiceLocator
    {
        private readonly IKernel _kernel;

        public NinjectServiceLocator()
        {
            _kernel = new StandardKernel(new QuizlrModule());
        }

        public HomeViewModel Home
        {
            get { return _kernel.Get<HomeViewModel>(); }
        }

        public QuizCrudViewModel QuizCrud
        {
            get { return _kernel.Get<QuizCrudViewModel>(); }
        }

        public QuestionCrudViewModel QuestionCrud
        {
            get { return _kernel.Get<QuestionCrudViewModel>(); }
        }
    }
}