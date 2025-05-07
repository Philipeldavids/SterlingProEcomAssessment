using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    // Cart.cs
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public List<CartItem> Items { get; set; } = new();
    }
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }   // ✅ Foreign Key
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }     // ✅ Navigation Property
    }
}
