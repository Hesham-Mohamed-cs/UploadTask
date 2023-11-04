using MatrixEC.Models.Context;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;

namespace MatrixEC.Repository.CategoryReprository
{
    public interface ICategoryReprositry
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void AddCategory(CategoryViewModel categoryVm);
        void UpdateCategory(EditCategoryViewModel categoryVm);
        bool CheckNameExists(string Name);
        void Save();
    }
}
