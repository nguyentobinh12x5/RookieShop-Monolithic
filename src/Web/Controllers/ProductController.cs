using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Product;
using Web.Commom.Request;
using Web.Service.IService;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProducts(query, cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductById(id, cancellationToken);

            return Ok(product);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto request, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProduct(request, cancellationToken);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto request, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateProduct(id, request, cancellationToken);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            await _productService.DeleteProduct(id, cancellationToken);
            return NoContent();
        }
    }
}
