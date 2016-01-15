using System.Collections.Generic;
using Quizlr.Domain.Model;

namespace Quizlr.Domain.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int categoryId);
        Category CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}