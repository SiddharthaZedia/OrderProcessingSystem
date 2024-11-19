using OrderProcessingSystem.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessingSystem.Model.Request
{
    public class UpdateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
