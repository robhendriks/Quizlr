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

        public QuestionCrudViewModel QuestionCrud
        {
            get { return _kernel.Get<QuestionCrudViewModel>(); }
        }
    }
}