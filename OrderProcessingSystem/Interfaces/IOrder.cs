using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;

namespace OrderProcessingSystem.Interfaces
{
    public interface IOrder
    {
        Task<IEnumerable<OrderDto>> GetOrders(CancellationToken cancellationToken);
        Task<OrderDto?> GetOrder(int id, CancellationToken cancellationToken);
        Task<int> CreateOrder(OrderRequest OrderRequest);
    }
}
