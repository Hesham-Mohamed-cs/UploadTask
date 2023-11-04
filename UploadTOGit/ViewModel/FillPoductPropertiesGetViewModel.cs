using MatrixEC.Models.Entiy;
using Microsoft.Build.Framework;

namespace MatrixEC.ViewModel
{
    public class FillPoductPropertiesGetViewModel
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName  { get; set; }
        
        public List<int>? proertyides = new List<int>();
        // public List<asd>? property = new List<asd>();

        public List<string>? propertItesNames = new List<string>();
    }
}
