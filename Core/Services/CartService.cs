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

        public async Task<bool> AddToCartAsync(string userId, string ProductId, int Quantity)
        {
            _cartRepo.AddToCartAsync(userId, ProductId, Quantity);
            return true;
        }

        public async Task<int> GetCartItemCountAsync(string userId)
        {
           var count = await _cartRepo.GetCartItemCountAsync(userId);
            return count;
           
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _cartRepo.GetCartItemsAsync(userId);
            return cartItems;
        }

        public async Task<bool> RemoveFromCartAsync(string cartItemId, string userId)
        {
            _cartRepo.RemoveFromCartAsync(cartItemId, userId);
            return true;
        }
    }
}
