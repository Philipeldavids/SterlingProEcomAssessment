using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace SterlingProEcommerce.Controllers
{
    public class ProductController : Controller
    {
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            
            TempData["Success"] = "Product added successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
