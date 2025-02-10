using Order_Management.DTOs.RequesDTOs;
using Order_Management.IRepositories;
using Order_Management.IServices;
using Order_Management.Models;
using Order_Management.Repositories;

namespace Order_Management.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
        {
            var customer = new Customer();
            customer.Name = customerRequestDTO.Name;
            customer.Email = customerRequestDTO.Email;
            customer.Phone = customerRequestDTO.Phone;
            customer.Address = customerRequestDTO.Address;
            customer.CreatedAt = DateTime.UtcNow;

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            existingCustomer.Name = customerRequestDTO.Name;
            existingCustomer.Email = customerRequestDTO.Email;
            existingCustomer.Address = customerRequestDTO.Address;
            existingCustomer.Phone = customerRequestDTO.Phone;

            var customerData = _customerRepository.UpdateCustomerAsync(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var success = await _customerRepository.DeleteCustomerAsync(id);
            if (!success) throw new KeyNotFoundException("Customer not found.");
            return success;
        }
    }
}
