using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SterlingProEcommerce.Models.ViewModel;
using System.Security.Claims;

namespace SterlingProEcommerce.Controllers
{
 
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            
            //if(userId == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            var items = await _cartService.GetCartItemsAsync();

            List<CartItemViewModel> Items = new List<CartItemViewModel>();
            CartItemViewModel model = new CartItemViewModel();
            foreach (var item in items)
            {

                model.Quantity = item.Quantity;
                model.Price = item.Product.Price;
                model.Id = item.Id;
                model.ProductName = item.Product.Name;
                

                Items.Add(model);
               
            }
            return View(new CartViewModel { Items = Items });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartAddRequest request)
        {
                    

            await _cartService.AddToCartAsync (request.ProductId, request.Quantity);
            return Ok();
        }

        [HttpGet]
        public async Task<ContentResult> GetCartCount()        {
           
            var count = await _cartService.GetCartItemCountAsync();
            return Content(count.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return RedirectToAction("Index");
        }
    }


}
