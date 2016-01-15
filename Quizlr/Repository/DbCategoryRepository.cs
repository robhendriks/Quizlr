using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Quizlr.Domain.Model;
using Quizlr.Domain.Repository;

namespace Quizlr.Repository
{
    public class DbCategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            return QuizlrContext.Current.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return QuizlrContext.Current.Categories.FirstOrDefault(o => o.CategoryId == categoryId);
        }

        public Category CreateCategory(Category category)
        {
            var ctx = QuizlrContext.Current;
            ctx.Categories.Add(category);
            ctx.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            var ctx = QuizlrContext.Current;
            ctx.Categories.Attach(category);
            ctx.Entry(category).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            var ctx = QuizlrContext.Current;
            ctx.Categories.Remove(category);
            ctx.SaveChanges();
        }
    }
}