using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixEC.Models.Entiy
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
    }
}
