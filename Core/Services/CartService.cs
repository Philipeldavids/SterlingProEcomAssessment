using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;

        public CartService(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<bool> AddToCartAsync(string ProductId, int Quantity)
        {
            _cartRepo.AddToCartAsync(ProductId, Quantity);
            return true;
        }

        public async Task<int> GetCartItemCountAsync()
        {
           var count = await _cartRepo.GetCartItemCountAsync();
            return count;
           
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var cartItems = await _cartRepo.GetCartItemsAsync();
            return cartItems;
        }

        public async Task<bool> RemoveFromCartAsync(string cartItemId)
        {
            _cartRepo.RemoveFromCartAsync(cartItemId);
            return true;
        }
    }
}
