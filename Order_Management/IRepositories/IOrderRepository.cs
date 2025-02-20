﻿using Order_Management.DTOs.RequesDTOs;
using Order_Management.Models;

namespace Order_Management.IRepositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order Order);
        Task UpdateOrderAsync(Order Order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
