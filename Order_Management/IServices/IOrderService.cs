using Order_Management.DTOs.RequesDTOs;
using Order_Management.Models;

namespace Order_Management.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderRequestDTO OrderRequestDTO);
        Task UpdateOrderAsync(OrderRequestDTO OrderRequestDTO, int id);
        Task<bool> DeleteOrderAsync(int id);
    }
}
