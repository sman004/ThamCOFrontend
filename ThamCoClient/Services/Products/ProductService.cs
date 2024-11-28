using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThamCoClient.Services.Products
{
   public class ProductService : IProductService
    {
         private readonly HttpClient _httpClient; 

         public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://products-api-cxarauf0d8hzc0fp.uksouth-01.azurewebsites.net/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>() ?? new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductDto>() 
                   ?? throw new HttpRequestException($"Product with ID {id} not found.");
        }
        
    }
}