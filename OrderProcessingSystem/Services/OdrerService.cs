using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.Domain;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;
using OrderProcessingSystem.Repositories;
namespace OrderProcessingSystem.Services
{
    public class Employee
    {
        public string dept { get; set; }
        public decimal Salary { get; set; }
        public decimal EmpName { get; set; }
    }
    public class OdrerService : IOrder
    {
        private readonly OrderProcessingDbContext _context;
        private readonly IMapper _mapper;

        public OdrerService(OrderProcessingDbContext context, IMapper autoMapper)
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
            var order = await _context.Orders.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order != null)
                return _mapper.Map<Order, OrderDto>(order);

            List<Employee> employee = new List<Employee>();
            employee.Add(new Employee());

            int i = 0;
            var emp = employee.GroupBy(x=> new { x.EmpName, x.dept }).OrderByDescending(x => x.Key).Skip(7).Take(1).FirstOrDefault();
            return null;
        }
        public OrderDto? GetOrderById(int id, CancellationToken cancellationToken)
        {
            var order =  _context.Orders.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product).FirstOrDefault(o => o.OrderId == id);
            if (order != null)
                return _mapper.Map<Order, OrderDto>(order);

            List<Employee> employee = new List<Employee>();
            employee.Add(new Employee());

            var emp = employee.GroupBy(x => new { x.EmpName, x.dept }).OrderByDescending(x => x.Key).Skip(7).Take(1).FirstOrDefault();
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
