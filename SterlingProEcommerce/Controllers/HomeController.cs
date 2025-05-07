using System.Diagnostics;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SterlingProEcommerce.Models;
using SterlingProEcommerce.Models.ViewModel;

namespace SterlingProEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> productViewModel = new List<ProductViewModel>();
            ProductViewModel prod = new ProductViewModel();
            var result = await _productService.GetAllProducts();
            if (result!= null)
            {
                foreach (var product in result) 
                { 
                    
                        prod.Id = product.Id.ToString();
                        prod.Name = product.Name;
                        prod.Description = product.Description;
                        prod.Price = product.Price;
                        prod.ProductImageUrl = product.ProductImageUrl;

                        productViewModel.Add(prod);
                }
               
                return View(productViewModel);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
