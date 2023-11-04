using MatrixEC.Migrations;
using MatrixEC.Models.Entiy;
using MatrixEC.ViewModel;

namespace MatrixEC.Repository.PropertyReprositry
{
    public interface IPropertyReprositry
    {
        IEnumerable<Property> GetAllProperties();
        Property GetPropertyById(int PropertyId);
        void AddProperty(AddProertyViewModel proertyViewModel);
        void UpdateProperty(EditPrpertyViewModel proertyViewModel);
        bool CheckNameExists(string Name,int CategoryId);
        void Save();
    }
}
