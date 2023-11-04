using MatrixEC.Migrations;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;

namespace MatrixEC.Repository.ProductReprository
{
    public interface IProductReprositry
    {
        IEnumerable<Product> GetAllProducts();
        Product GetproductById(int PropertyId);
        void AddPoduct(AddProductViewModel ProductViewModel);
        void UpdateProduct(EditProductViewModel ProductViewModel);
        bool CheckNameExists(string Name,int CategoryId);
        FillPoductPropertiesGetViewModel DisplayproductWithPeroperty(int ProductId);
        void FillproductWithPeroperties(FillPoductPropertiesPostViewModel productPropertyvm);
        public productDetails productDetails(int productId);

        void Save();
    }
}
