using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThamCoClient.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);  
        ////jhjh  
    }
}