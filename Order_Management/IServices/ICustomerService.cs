using Order_Management.DTOs.RequesDTOs;
using Order_Management.Models;

namespace Order_Management.IServices
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerRequestDTO customerRequestDTO);
        Task UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
