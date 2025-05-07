using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    // Cart.cs
    public class Cart
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public List<CartItem> Items { get; set; } = new();
    }
    public class CartItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public Product Product { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
