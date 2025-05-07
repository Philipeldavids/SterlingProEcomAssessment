using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICartRepo
    {
        Task AddToCartAsync(string userId, string productId, int quantity);

        Task<List<CartItem>> GetCartItemsAsync(string userId);

        Task<int> GetCartItemCountAsync(string userId);

        Task RemoveFromCartAsync(string cartItemId, string userId);
    }
}
