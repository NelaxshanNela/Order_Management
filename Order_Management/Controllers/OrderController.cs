using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Management.DTOs.RequesDTOs;
using Order_Management.IServices;
using Order_Management.Models;

namespace Order_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var existingOrders = await _orderService.GetAllOrdersAsync();

                if (existingOrders == null || !existingOrders.Any())
                {
                    return Ok(new { success = true, message = "No orders found", data = new List<Order>() });
                }

                return Ok(new { success = true, message = "Orders retrieved successfully", data = existingOrders });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error fetching orders", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var existingOrder = await _orderService.GetOrderByIdAsync(id);

                if (existingOrder == null)
                {
                    return NotFound(new { success = false, message = "Order not found" });
                }

                return Ok(new { success = true, message = "Order retrieved successfully", data = existingOrder });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error fetching order", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequestDTO orderRequestDTO)
        {
            try
            {
                await _orderService.AddOrderAsync(orderRequestDTO);
                return Ok(new { success = true, message = "Order added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error adding order", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderRequestDTO orderRequestDTO)
        {
            try
            {
                await _orderService.UpdateOrderAsync(orderRequestDTO, id);
                return Ok(new { success = true, message = "Order updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error updating order", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var success = await _orderService.DeleteOrderAsync(id);
                if (success)
                    return Ok(new { success = true, message = "Order deleted successfully" });
                return NotFound(new { success = false, message = "Order not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error deleting order", error = ex.Message });
            }
        }
    }

}
