using Order_Management.DTOs.RequesDTOs;
using Order_Management.IRepositories;
using Order_Management.IServices;
using Order_Management.Models;

namespace Order_Management.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task AddOrderAsync(OrderRequestDTO orderRequestDTO)
        {
            if (orderRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(orderRequestDTO), "Order request cannot be null.");
            }

            var product = await _productRepository.GetProductByIdAsync(orderRequestDTO.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            if (product.Stock < orderRequestDTO.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock for the requested product.");
            }

            var order = new Order
            {
                ProductId = orderRequestDTO.ProductId,
                CustomerId = orderRequestDTO.CustomerId,
                Quantity = orderRequestDTO.Quantity,
                TotalPrice = orderRequestDTO.TotalPrice,
                OrderDate = DateTime.Now,
                Status = 0
            };

            product.Stock -= orderRequestDTO.Quantity;

            await _orderRepository.AddOrderAsync(order);
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task UpdateOrderAsync(OrderRequestDTO orderRequestDTO, int id)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            existingOrder.Status = orderRequestDTO.Status;
            existingOrder.Quantity = orderRequestDTO.Quantity;

            await _orderRepository.UpdateOrderAsync(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var success = await _orderRepository.DeleteOrderAsync(id);
            if (!success) throw new KeyNotFoundException("Order not found.");
            return success;
        }
    }


}
