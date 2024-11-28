using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThamCoClient.Models;
using ThamCoClient.Services.Products; // Make sure to include the service

namespace ThamCoClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        // Constructor injecting both ILogger and IProductService
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // Index action to display products
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductDto> products = null!;
            try
            {
                // Fetch products asynchronously from the service
                products = await _productService.GetProductsAsync();
            }
            catch (Exception ex)
            {
                // Log error if something goes wrong
                _logger.LogError($"An error occurred while retrieving products: {ex.Message}");
            }

            // Pass the list of products to the view
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
