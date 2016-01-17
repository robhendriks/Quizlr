namespace Quizlr.ViewModel
{
    public interface IViewModelLocator
    {
        T Get<T>() where T : class;
    }
}