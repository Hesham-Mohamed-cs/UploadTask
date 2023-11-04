using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixEC.ViewModel
{
    public class AddProertyViewModel
    {
        [Display(Name = "Property Name")]
        [Remote(action: "CheckNameExists", controller: "Property",AdditionalFields = "CategoryId", ErrorMessage = "Name Oready Exist in This Category ")]
        [MinLength(3, ErrorMessage = "category name must be more than 3 char ")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "category name must be less than 50 char ")]
        public string Name { get; set; }

        [Display( Name = " Category ")]
        public int CategoryId { get; set; }
    }
}
