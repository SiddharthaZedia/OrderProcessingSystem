using OrderProcessingSystem.Model.Domain;

namespace OrderProcessingSystem.Model.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
