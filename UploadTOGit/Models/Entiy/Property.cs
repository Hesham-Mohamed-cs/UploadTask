using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixEC.Models.Entiy
{
    public class Property
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
