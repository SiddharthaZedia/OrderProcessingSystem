using OrderProcessingSystem.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessingSystem.Model.Request
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
