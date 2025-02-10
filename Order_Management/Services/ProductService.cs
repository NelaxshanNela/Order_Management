using Order_Management.DTOs.RequesDTOs;
using Order_Management.IRepositories;
using Order_Management.IServices;
using Order_Management.Models;

namespace Order_Management.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(ProductRequestDTO productRequestDTO)
        {
            var product = new Product();
            product.Category = productRequestDTO.Category;
            product.Price = productRequestDTO.Price;
            product.Description = productRequestDTO.Description;
            product.Name = productRequestDTO.Name;
            product.Stock = productRequestDTO.Stock;

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductRequestDTO productRequestDTO, int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            existingProduct.Name = productRequestDTO.Name;
            existingProduct.Description = productRequestDTO.Description;
            existingProduct.Price = productRequestDTO.Price;
            existingProduct.Stock = productRequestDTO.Stock;
            existingProduct.Category = productRequestDTO.Category;

            var productData = _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var success = await _productRepository.DeleteProductAsync(id);
            if (!success) throw new KeyNotFoundException("Product not found.");
            return success;
        }
    }
}
