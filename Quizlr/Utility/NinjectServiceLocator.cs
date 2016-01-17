using System;
using System.Collections.Generic;
using Ninject;
using Quizlr.Repository;

namespace Quizlr.Utility
{
    public class NinjectServiceLocator
    {
        private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();
        private static readonly IKernel Kernel = new StandardKernel(new QuizlrModule());

        private static bool HasInstance<T>() where T : class
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