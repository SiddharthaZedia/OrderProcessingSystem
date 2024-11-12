using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Model.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        // Total price of the order (calculated)
        public decimal TotalPrice => CalculateTotalPrice();

        // Method to calculate the total price of the order
        private decimal CalculateTotalPrice()
        {
            decimal total = 0;
            foreach (var item in OrderItems)
            {
                total += item.Quantity * item.Product.Price;
            }
            return total;
        }
    }

}
