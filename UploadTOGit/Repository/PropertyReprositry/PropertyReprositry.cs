using MatrixEC.Models.Context;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MatrixEC.Repository.PropertyReprositry
{
    public class PropertyReprositry : IPropertyReprositry
    {
        private readonly MatrixContext context;

        public PropertyReprositry(MatrixContext context)
        {
            this.context = context;
        }

        public void AddProperty(AddProertyViewModel proertyViewModel)
        {
            try
            {
                Property NewProperty = new Property();
                NewProperty.Name = proertyViewModel.Name;
                NewProperty.CategoryId = proertyViewModel.CategoryId;
                context.Properties.Add(NewProperty);
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckNameExists(string Name,int CategoryId)
        {
            return context.Properties.Where(c => c.Name == Name && c.CategoryId == CategoryId).SingleOrDefault() == null ? true : false;

        }

        public IEnumerable<Property> GetAllProperties()
        {
            return context.Properties.Include(p => p.Category).ToList();
        }

        public Property GetPropertyById(int PropertyId)
        {
            return context.Properties.Include(p => p.Category).SingleOrDefault(p => p.Id == PropertyId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateProperty(EditPrpertyViewModel proertyViewModel)
        {
            try
            {
                Property updatedProperty = GetPropertyById(proertyViewModel.Id);
                if (updatedProperty == null)
                {
                    throw new Exception(" Invalid Category  Id ");
                }
                updatedProperty.Name = proertyViewModel.Name;
                updatedProperty.CategoryId = proertyViewModel.CategoryId;
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
