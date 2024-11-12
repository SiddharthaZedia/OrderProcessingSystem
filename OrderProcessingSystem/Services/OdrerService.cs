using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.Domain;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;
using OrderProcessingSystem.Repositories;
namespace OrderProcessingSystem.Services
{
    public class OdrerService : IOrder
    {
        private readonly OrderPrcossingDbContext _context;
        private readonly IMapper _mapper;

        public OdrerService(OrderPrcossingDbContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }
        public async Task<IEnumerable<OrderDto>> GetOrders(CancellationToken cancellationToken)
        {
            return await _context.Orders.AsNoTracking().Select(x => _mapper.Map<OrderDto>(x)).ToListAsync(cancellationToken);
        }
        public async Task<OrderDto?> GetOrder(int id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order != null)
                return _mapper.Map<Order, OrderDto>(order);
            return null;
        }
        public async Task<int> CreateOrder(OrderRequest OrderRequest)
        {
            var order = _mapper.Map<OrderRequest, Order>(OrderRequest);
            _context.Orders.Add(order);
            if (await _context.SaveChangesAsync() > 0)
                return order.OrderId;
            return 0;
        }
    }
}
