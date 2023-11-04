using MatrixEC.Models.Entiy;
using MatrixEC.Repository.CategoryReprository;
using MatrixEC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MatrixEC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryReprositry reprositry;
        public CategoryController(ICategoryReprositry reprositry)
        {
            this.reprositry = reprositry;
        }
        public IActionResult Index()
        {
            return View(reprositry.GetAllCategories());
        }

        public IActionResult CheckNameExists(string Name)
        {
            if(reprositry.CheckNameExists(Name))
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var Categories = reprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name");
            return View(new CategoryViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryViewModel inp )
        {
            var Categories = reprositry.GetAllCategories().Select(c => new  { Name = c.Name, Id = c.Id }).ToList();

            if (ModelState.IsValid)
            {
              try
                {
                    reprositry.AddCategory(inp);
                    reprositry.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("parentCategoryId", "Invalid Parent  Category ");
                    ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name");
                    return View(inp);
                }
            }
            ViewBag.ParentCategories = new SelectList(Categories , "Id","Name");
            return View(inp);
        }



        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = reprositry.GetCategoryById(Id);
            if (category == null)
                return NotFound();
            EditCategoryViewModel categoryViewModel = new EditCategoryViewModel()
            {
                Name = category.Name,
                parentCategoryId = category.ParentCategoryId
            };
            var Categories = reprositry.GetAllCategories().Where(c => c.Id != Id).Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name", category);



            return View(categoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel inp)
        {
            var Categories = reprositry.GetAllCategories().Where(c => c.Id != inp.Id).Select(c => new { Name = c.Name, Id = c.Id }).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    reprositry.UpdateCategory(inp);
                    reprositry.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("DB", " Invalid Data ");
                    ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name", inp);
                    return View(inp);
                }
            }
            ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name", inp);
            return View(inp);
        }
    }
}
