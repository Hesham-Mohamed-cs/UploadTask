using MatrixEC.Models.Context;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace MatrixEC.Repository.ProductReprository
{
    public class ProductReprositry : IProductReprositry
    {
        private readonly MatrixContext context;

        public ProductReprositry(MatrixContext context)
        {
            this.context = context;
        }

        public void AddPoduct(AddProductViewModel ProductViewModel)
        {
            try
            {
                Product NewProduct= new Product();
                NewProduct.Name = ProductViewModel.Name;
                NewProduct.CategoryId = ProductViewModel.CategoryId;
                NewProduct.Price = ProductViewModel.Price;
                context.Products.Add(NewProduct);
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckNameExists(string Name, int CategoryId)
        {
            return context.Products.Where(c => c.Name == Name && c.CategoryId == CategoryId).SingleOrDefault() == null ? true : false;

        }
        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.Include(p => p.Category).ToList();
        }

        public Product GetproductById(int PropertyId)
        {
            return context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == PropertyId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateProduct(EditProductViewModel ProductViewModel)
        {
            try
            {
                Product updatedProduct = GetproductById(ProductViewModel.Id);
                if (updatedProduct == null)
                {
                    throw new Exception(" Invalid Category  Id ");
                }
                updatedProduct.Name = ProductViewModel.Name;
                updatedProduct.CategoryId = ProductViewModel.CategoryId;
                updatedProduct.Price = ProductViewModel.Price;
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public FillPoductPropertiesGetViewModel DisplayproductWithPeroperty(int ProductId)
        {
            var product = GetproductById(ProductId);
            var data = context.Categories.Where(c => c.Id == product.CategoryId)
                .Include(c => c.Properties).SelectMany(c => c.Properties).ToList();

            FillPoductPropertiesGetViewModel vm = new FillPoductPropertiesGetViewModel();
            vm.ProductName = product.Name;
            vm.ProductId = product.Id;
            vm.CategoryName = product.Category.Name;
            foreach (var item in data)
            {
                vm.proertyides.Add(item.Id);
                vm.propertItesNames.Add(item.Name);
            }

            return vm;
        }

        public void FillproductWithPeroperties(FillPoductPropertiesPostViewModel productPropertyvm)
        {
            for (int i = 0; i < productPropertyvm.proertyIds.Count; i++)
            {
                context.PropertyValues.Add(new PropertyValue()
                {
                    ProductId = productPropertyvm.ProductId,
                    PropertyId = productPropertyvm.proertyIds[i],
                    Value = productPropertyvm.propvalues[i]

                });
            }
            Save();
        }

        public productDetails productDetails(int productId)
        {
            var property = context.PropertyValues.Where(pv =>pv.ProductId == productId).Include(p=>p.Property).ToList();
            var product = GetproductById(productId);
            productDetails productDetails = new productDetails();
            productDetails.ProductName = product.Name ;
            productDetails.productCategory = product.Category.Name;
            foreach (var item in property)
            {
                productDetails.PropertyNames.Add(item.Property.Name);
                productDetails.PropertyValues.Add(item.Value);
            }
            return productDetails;
        }
    }
}
