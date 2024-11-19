using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.Domain;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Repositories;
namespace OrderProcessingSystem.Services
{
    public class CustomerService:ICustomer
    {
        private readonly OrderProcessingDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(OrderProcessingDbContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }
        public async Task<IEnumerable<CustomerDto>> GetCustomers(CancellationToken cancellationToken)
        {
            return await _context.Customers.AsNoTracking().Select(x => _mapper.Map<CustomerDto>(x)).ToListAsync(cancellationToken);
        }
        public async Task<CustomerDto?> GetCustomer(int id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
                return _mapper.Map<Customer, CustomerDto>(customer);
            return null;
        }
        public async Task<int> CreateCustomer(CustomerRequest customerRequest)
        {
            var customer = _mapper.Map<CustomerRequest, Customer>(customerRequest);
            _context.Customers.Add(customer);
            if (await _context.SaveChangesAsync() > 0)
                return customer.CustomerId;
            return 0;
        }
    }
}
