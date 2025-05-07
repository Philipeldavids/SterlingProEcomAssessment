using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SterlingProEcommerce.Models.ViewModel;
using System.Security.Claims;

namespace SterlingProEcommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = await _cartService.GetCartItemsAsync(userId);

            List<CartItemViewModel> Items = new List<CartItemViewModel>();
            foreach (var item in items)
            { 
                foreach(var itm in Items)
                {
                    itm.Quantity = item.Quantity;
                    itm.Price = item.Product.Price;
                    itm.Id = item.ProductId;
                    itm.ProductName = item.Product.Name;
                }
            }
            return View(new CartViewModel { Items = Items });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartAddRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(userId, request.ProductId, request.Quantity);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var count = await _cartService.GetCartItemCountAsync(userId);
            return Content(count.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveFromCartAsync(cartItemId, userId);
            return RedirectToAction("Index");
        }
    }


}
