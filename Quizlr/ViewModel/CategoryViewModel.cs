using Quizlr.Domain.Model;

namespace Quizlr.ViewModel
{
    public class CategoryViewModel : SimpleViewModel<Category>
    {
        public CategoryViewModel(Category poco = null) : base(poco)
        {
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

        public string Name
        {
            get { return Poco.Name; }
            set
            {
                Poco.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
    }
}