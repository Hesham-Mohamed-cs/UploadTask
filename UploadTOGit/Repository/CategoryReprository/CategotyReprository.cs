using MatrixEC.Models.Context;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MatrixEC.Repository.CategoryReprository
{
    public class CategotyReprository : ICategoryReprositry
    {
        private readonly MatrixContext context; 

        public CategotyReprository(MatrixContext context)
        {
            this.context = context; 
        }

        public void AddCategory(CategoryViewModel categoryVm)
        {
            try
            {
                Category category = new Category();
                category.Name = categoryVm.Name;
                category.ParentCategoryId = categoryVm.parentCategoryId;
                context.Categories.Add(category);
                Save();
            }
            catch (Exception ex) 
            {
                throw ex ;
            }

        }

        public void UpdateCategory(EditCategoryViewModel categoryVm)
        {
            try
            {
                Category category = GetCategoryById(categoryVm.Id);
                if (category == null)
                {
                    throw new Exception("Invalid Category  Id ");
                }
                category.Name = categoryVm.Name;
                category.ParentCategoryId =categoryVm.parentCategoryId;
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return context.Categories.Include(c => c.ParentCategory).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return context.Categories.Include(c => c.ParentCategory).SingleOrDefault(c => c.Id == categoryId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public bool CheckNameExists(string Name)
        
        {
            return context.Categories.Where(c => c.Name == Name).SingleOrDefault() == null ? true : false ;
            
        }
    }
}
