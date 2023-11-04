using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MatrixEC.ViewModel
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "category name must be more than 3 char ")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "category name must be less than 50 char ")]
        public string Name { get; set; }
        public int? parentCategoryId { get; set; }
    }
}
