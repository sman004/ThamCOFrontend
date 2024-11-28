using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThamCoClient.Services.Products
{
   public class ProductServiceFake : IProductService
    {
        private readonly List<ProductDto> _products = new()
        {
            new ProductDto { Id = 1, Name = "Fake Product 1", Price = 9.99m, Description = "This is a fake product." },
            new ProductDto { Id = 2, Name = "Fake Product 2", Price = 19.99m, Description = "This is another fake product." }
        };

        public Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return Task.FromResult<IEnumerable<ProductDto>>(_products);
        }

        public Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product ?? throw new KeyNotFoundException($"Product with ID {id} not found."));
        }
    }
}