using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.DTOs.RequesDTOs;
using Order_Management.IServices;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var existingProducts = await _productService.GetAllProductsAsync();

                if (existingProducts == null || !existingProducts.Any())
                {
                    return BadRequest("No products found");
                }
                return Ok(existingProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving products", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(id);

                if (existingProduct == null)
                {
                    return BadRequest("Invalid product ID");
                }
                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error retrieving product", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequestDTO productRequestDTO)
        {
            try
            {
                await _productService.AddProductAsync(productRequestDTO);
                return Ok(new { success = true, message = "Product added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error adding product", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequestDTO productRequestDTO)
        {
            try
            {
                await _productService.UpdateProductAsync(productRequestDTO, id);
                return Ok(new { success = true, message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error updating product", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return Ok(new { success = true, message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting product", error = ex.Message });
            }
        }
    }

}
