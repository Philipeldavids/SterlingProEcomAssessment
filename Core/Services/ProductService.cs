using Core.DTO;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IProductRepo productRepo, ICloudinaryService cloudinaryService)
        {
            _productRepo = productRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _productRepo.GetProducts();
            return products;
        }

        public async Task<bool> AddProduct(ProductDto product)
        {
            Product product1 = new Product();
            product1.Name = product.Name;
            product1.Description = product.Description;
            product1.Price = product.Price;
            product1.Category = product.Category;
            product1.UnitCost = product.UnitCost;
            product1.Brand = product.Brand;
            product1.Quantity = product.Quantity;
            product1.CreatedAt = DateTime.Now;
           
            var getImageUrl = await _cloudinaryService.UploadFileAsync(new MediaFileUploadRequest { File = product.ProductImage});
            product1.ProductImageUrl = getImageUrl.SecureUrl.ToString();


            var result = await _productRepo.AddProducts(product1);
            return result;

        }
    }
}
