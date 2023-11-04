using MatrixEC.Models.Entiy;
using MatrixEC.Repository.CategoryReprository;
using MatrixEC.Repository.PropertyReprositry;
using MatrixEC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MatrixEC.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyReprositry reprositry;
        private readonly ICategoryReprositry categoryReprositry;

        public PropertyController(IPropertyReprositry reprositry , ICategoryReprositry categoryReprositry)
        {
            this.reprositry = reprositry;
            this.categoryReprositry = categoryReprositry;
        }
        public IActionResult CheckNameExists(string Name, int categoryId)
        {
            if (reprositry.CheckNameExists(Name,categoryId))
            {
                return Json(true);
            }
            return Json(false);
        }
        public IActionResult Index()
        {
            return View(reprositry.GetAllProperties());
        }
        [HttpGet]
        public IActionResult Add()
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
            ViewBag.flag= Categories.Count > 0 ? true:false;
            return View(new AddProertyViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddProertyViewModel inp)
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    reprositry.AddProperty(inp);
                    reprositry.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CategoryId", "Invalid  Category Id ");
                    ViewBag.ParentCategories = new SelectList(Categories, "Id", "Name");
                    return View(inp);
                }
            }
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
            return View(inp);
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var property = reprositry.GetPropertyById(Id);
            if (property == null)
                return NotFound();
            EditPrpertyViewModel PropertyViewModel = new EditPrpertyViewModel()
            {
                Name = property.Name,
                CategoryId = property.CategoryId
            };
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            var category = categoryReprositry.GetCategoryById(property.CategoryId);
            ViewBag.Categories = new SelectList(Categories, "Id", "Name", category);
            return View(PropertyViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditPrpertyViewModel inp)
        {
            var Categories = categoryReprositry.GetAllCategories().Select(c => new { Name = c.Name, Id = c.Id }).ToList();
            var category = categoryReprositry.GetCategoryById(inp.CategoryId);
            if (ModelState.IsValid)
            {
                try
                {
                    reprositry.UpdateProperty(inp);
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
    }
}
