using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThamCoClient.Services.Products; 

namespace ThamCoClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /products/
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ProductDto> products = null!;
            try
            {
                products = await _productService.GetProductsAsync();
            }
            catch
            {
                // If you prefer, you can handle the error here by returning a specific view
                // For now, we'll just set an empty list
                products = Array.Empty<ProductDto>();
            }

            return View(products.ToList());
        }

        // GET: /products/details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {  
                return BadRequest();
            }

            try
            {
                var product = await _productService.GetProductByIdAsync(id.Value);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch
            {
                // Handle errors (you could display a custom error page here if needed)
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
