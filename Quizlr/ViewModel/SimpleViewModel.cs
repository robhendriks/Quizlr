using GalaSoft.MvvmLight;

namespace Quizlr.ViewModel
{
    public abstract class SimpleViewModel<T> : ViewModelBase where T : class, new()
    {
        public readonly T Poco;

        protected SimpleViewModel(T poco = null)
        {
            Poco = poco ?? new T();
        }

        public static implicit operator T(SimpleViewModel<T> viewModel)
        {
            return viewModel?.Poco;
        }
    }
}