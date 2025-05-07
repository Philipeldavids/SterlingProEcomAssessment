using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class CartAddRequest
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public int Quantity { get; set; } = 1;
    }
}
