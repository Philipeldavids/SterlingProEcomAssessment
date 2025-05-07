using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _context;

        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
      

            public async Task AddToCartAsync(string userId, string productId, int quantity)
            {
                var existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var newItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        UserId = userId
                    };
                    await _context.CartItems.AddAsync(newItem);
                }

                await _context.SaveChangesAsync();
            }

            public async Task<List<CartItem>> GetCartItemsAsync(string userId)
            {
                return await _context.CartItems
                    .Include(c => c.Product)
                    .Where(c => c.UserId == userId)                    
                    .ToListAsync();
            }

            public async Task<int> GetCartItemCountAsync(string userId)
            {
                return await _context.CartItems
                    .Where(c => c.UserId == userId)
                    .SumAsync(c => c.Quantity);
            }

            public async Task RemoveFromCartAsync(string cartItemId, string userId)
            {
                var item = await _context.CartItems
                    .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
        }


    }

