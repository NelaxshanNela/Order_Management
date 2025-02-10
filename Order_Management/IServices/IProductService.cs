using Order_Management.DTOs.RequesDTOs;
using Order_Management.Models;

namespace Order_Management.IServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductRequestDTO productRequestDTO);
        Task UpdateProductAsync(ProductRequestDTO productRequestDTO, int id);
        Task<bool> DeleteProductAsync(int id);
    }
}
