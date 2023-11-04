using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MatrixEC.ViewModel
{
    public class AddProductViewModel
    {
        [Display(Name = "Property Name")]
        [Remote(action: "CheckNameExists", controller: "product", AdditionalFields = "CategoryId", ErrorMessage = " Product Name Oready Exist in This Category ")]
        [MinLength(3, ErrorMessage = "category name must be more than 3 char ")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "category name must be less than 50 char ")]
       

        public string Name { get; set; }

        [Display(Name = "product Price In Egypt")]

        [Range(minimum:100, maximum:100_000 , ErrorMessage ="Prpduct Price Must Be Betwwen 100 and 100,100 Egyption bound")]
        public decimal Price { get; set; }
        [Display(Name ="product Category")]
        [Required(ErrorMessage ="Please Slecet Product  Category ")]
        public int CategoryId { get; set; }
    }
}
