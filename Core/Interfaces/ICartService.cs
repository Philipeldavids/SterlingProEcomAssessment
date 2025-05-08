using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(string productId, int quantity);
        Task<List<CartItem>> GetCartItemsAsync();
        Task<int> GetCartItemCountAsync();
        Task<bool> RemoveFromCartAsync(string cartItemId);

    }

}
