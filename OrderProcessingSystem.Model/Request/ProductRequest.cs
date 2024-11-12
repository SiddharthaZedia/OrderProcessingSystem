using OrderProcessingSystem.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessingSystem.Model.Request
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
