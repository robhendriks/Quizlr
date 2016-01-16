using System;
using System.Collections.Generic;
using Ninject;
using Quizlr.Repository;

namespace Quizlr.ViewModel
{
    public class NinjectServiceLocator
    {
        private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();
        private static readonly IKernel Kernel = new StandardKernel(new QuizlrModule());

        public HomeViewModel Home
        {
            get { return GetInstance<HomeViewModel>(); }
        }

        public PlayViewModel Play
        {
            get { return GetInstance<PlayViewModel>(); }
        }

        public ResultViewModel Result
        {
            get { return GetInstance<ResultViewModel>(); }
        }

        public ResultsViewModel Results
        {
            get { return GetInstance<ResultsViewModel>(); }
        }

        public QuizCrudViewModel QuizCrud
        {
            get { return GetInstance<QuizCrudViewModel>(); }
        }

        public QuestionCrudViewModel QuestionCrud
        {
            get { return GetInstance<QuestionCrudViewModel>(); }
        }

        public static bool HasInstance<T>() where T : class
        {
            return Instances.ContainsKey(typeof (T));
        }

        public static T GetInstance<T>() where T : class
        {
            var type = typeof (T);
            if (HasInstance<T>())
                return Instances[type] as T;
            var instance = Kernel.Get<T>();
            Instances.Add(type, instance);
            return instance;
        }
    }
}