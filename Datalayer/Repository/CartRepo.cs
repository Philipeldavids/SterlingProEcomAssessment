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
      

            public async Task AddToCartAsync(string productId, int quantity)
            {
                var existingItem = _context.CartItems.Where(c => c.ProductId == productId)
                    .FirstOrDefault();

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
                        
                    };
                    _context.CartItems.Add(newItem);
                }

                 _context.SaveChanges();
            }

            public async Task<List<CartItem>> GetCartItemsAsync()
            {
                return _context.CartItems
                    .Include(c => c.Product)                                       
                    .ToList();
            }

            public async Task<int> GetCartItemCountAsync()
            {
                return _context.CartItems                    
                    .Sum(c => c.Quantity);
            }

            public async Task RemoveFromCartAsync(string cartItemId)
            {
                var item = _context.CartItems.Where(c => c.Id == cartItemId)
                    .FirstOrDefault();

                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    _context.SaveChanges();
                }
            }
        }


    }

