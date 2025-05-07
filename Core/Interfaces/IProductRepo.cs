using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<bool> AddProducts(Product product);
        Task<List<Product>> GetProducts();
    }
}
