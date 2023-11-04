using MatrixEC.Models.Context;
using MatrixEC.Models.Entiy;
using MatrixEC.Repository.CategoryReprository;
using MatrixEC.Repository.ProductReprository;
using MatrixEC.Repository.PropertyReprositry;
using MatrixEC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace MatrixEC.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductReprositry reprositry;
        private readonly ICategoryReprositry categoryReprositry;
        public ProductController(IProductReprositry reprositry, ICategoryReprositry categoryReprositry )
        {
            this.reprositry = reprositry;
            this.categoryReprositry = categoryReprositry;
        }
        public IActionResult CheckNameExists(string Name, int categoryId)
        {
            if (reprositry.CheckNameExists(Name, categoryId))
            {
                return Json(true);
            }
            return Json(false);
        }
        public IActionResult Index()
        {
            return View(reprositry.GetAllProducts());
        }


        [HttpGet]
        public IActionResult Add()
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
            ViewBag.flag = Categories.Count > 0 ? true : false;
            return View(new AddProductViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddProductViewModel inp)
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    reprositry.AddPoduct(inp);
                    reprositry.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CategoryId", "Invalid  Category Id ");
                    ViewBag.Categories = new SelectList(Categories, "Id", "Name");
                    return View(inp);
                }
            }
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
            return View(inp);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var product = reprositry.GetproductById(Id);
            if (product == null)
                return NotFound();
            EditProductViewModel productViewModel = new EditProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId 
            };
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            var category = categoryReprositry.GetCategoryById(product.CategoryId);
            ViewBag.Categories = new SelectList(Categories, "Id", "Name", category);
            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductViewModel inp)
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            var category = categoryReprositry.GetCategoryById(inp.CategoryId);
            if (ModelState.IsValid)
            {
                try
                {
                    reprositry.UpdateProduct(inp);
                    reprositry.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("DB", " Invalid Data ");
                    ViewBag.Categories = new SelectList(Categories, "Id", "Name", category);
                    return View(inp);
                }
            }
            ViewBag.Categories = new SelectList(Categories, "Id", "Name", category);
            return View(inp);
        }
        [HttpGet]
        public IActionResult FillProperties(int Id )
        {       
            return View(reprositry.DisplayproductWithPeroperty(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FillProperties(FillPoductPropertiesPostViewModel productPropertyvm)
        {
            reprositry.FillproductWithPeroperties(productPropertyvm);
            return RedirectToAction("Index");
        }
    }
}
