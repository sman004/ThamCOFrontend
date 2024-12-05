using System.Net.Http.Headers;

namespace ThamCoClient.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private string? _cachedToken;
        private DateTime _tokenExpiry;

        public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        record TokenDto(string access_token, string token_type, int expires_in);

        private async Task<string> GetTokenAsync()
        {
            if (_cachedToken != null && DateTime.UtcNow < _tokenExpiry)
            {
                return _cachedToken;
            }

            var tokenClient = _clientFactory.CreateClient();
            var authBaseAddress = _configuration["Auth:Authority"];
            tokenClient.BaseAddress = new Uri(authBaseAddress);

            var tokenParams = new Dictionary<string, string> {
                { "grant_type", "client_credentials" },
                { "client_id", _configuration["Auth:ClientId"] },
                { "client_secret", _configuration["Auth:ClientSecret"] },
                { "audience", _configuration["Services:Values:AuthAudience"] },
            };
            var tokenForm = new FormUrlEncodedContent(tokenParams);
            var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
            tokenResponse.EnsureSuccessStatusCode();
            var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();

            _cachedToken = tokenInfo?.access_token;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenInfo?.expires_in ?? 0);

            return _cachedToken!;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var token = await GetTokenAsync();

            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("api/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>() ?? new List<ProductDto>();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var token = await GetTokenAsync();

            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductDto>()
                   ?? throw new HttpRequestException($"Product with ID {id} not found.");
        }
    }
}

/****************working code before i added security88


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

**/