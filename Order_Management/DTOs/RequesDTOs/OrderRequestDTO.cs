using Order_Management.Enums;
using Order_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.DTOs.RequesDTOs
{
    public class OrderRequestDTO
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
