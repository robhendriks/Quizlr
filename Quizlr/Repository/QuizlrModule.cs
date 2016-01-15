using Ninject.Modules;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class QuizlrModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuizRepository>().To<DbQuizRepository>();
            Bind<ICategoryRepository>().To<DbCategoryRepository>();
            Bind<IQuestionRepository>().To<DbQuestionRepository>();
            Bind<IQuizQuestionRepository>().To<DbQuizQuestionRepository>();
            Bind<IAnswerRepository>().To<DbAnswerRepository>();
        }
    }
}