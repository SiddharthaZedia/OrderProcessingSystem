using OrderProcessingSystem.Model.Domain;

namespace OrderProcessingSystem.Model.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
