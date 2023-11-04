using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixEC.Models.Entiy
{
    public class PropertyValue
    {
        public int Id { get; set; }
        [MaxLength(70)]
        public string Value { get; set; }

        [ForeignKey("Property")]
        public int? PropertyId { get; set; }
        public Property? Property { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        
    }
}
