using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Order_Management.Enums;

namespace Order_Management.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; }
    }
}
