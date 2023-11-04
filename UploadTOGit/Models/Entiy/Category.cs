using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixEC.Models.Entiy
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }
        public List<Property> Properties { get; set; }
    }
}
