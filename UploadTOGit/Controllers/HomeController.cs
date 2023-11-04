using MatrixEC.Models;
using MatrixEC.Repository.ProductReprository;
using MatrixEC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatrixEC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductReprositry productReprositry;
        public HomeController( IProductReprositry productReprositry )
        {
            this.productReprositry = productReprositry;
        }

        public IActionResult Index()
        {
            return View(productReprositry.GetAllProducts());
        }
        public IActionResult Details (int Id)
        {
            
            return View(productReprositry.productDetails(Id));
        }


        public IActionResult filterByName(string productName)
        {
            if(String.IsNullOrEmpty(productName))
            {
                return PartialView("_FilterByName", productReprositry.GetAllProducts());
            }
            
            var data = productReprositry.GetAllProducts().Where(p=> p.Name.ToLower().Contains(productName.ToLower())).ToList();
           

            ViewBag.Flag = data.Count > 0? true:false;


          return PartialView("_FilterByName", data);
        }

    }
}