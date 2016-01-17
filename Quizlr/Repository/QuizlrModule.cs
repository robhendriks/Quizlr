using Ninject.Modules;
using Quizlr.Domain.Repository;
using Quizlr.ViewModel;

namespace Quizlr.Repository
{
    public class QuizlrModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IViewModelLocator>().To<NinjectViewModelLocator>();
            Bind<IQuizRepository>().To<DbQuizRepository>();
            Bind<ICategoryRepository>().To<DbCategoryRepository>();
            Bind<IQuestionRepository>().To<DbQuestionRepository>();
            Bind<IQuizQuestionRepository>().To<DbQuizQuestionRepository>();
            Bind<IAnswerRepository>().To<DbAnswerRepository>();
            Bind<IQuizInstanceRepository>().To<DbQuizInstanceRepository>();
            Bind<IQuestionInstanceRepository>().To<DbQuestionInstanceRepository>();
            Bind<IAnswerInstanceRepository>().To<DbAnswerInstanceRepository>();
        }
    }
}