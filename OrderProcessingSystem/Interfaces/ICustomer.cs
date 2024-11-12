using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Model.DTO;

namespace OrderProcessingSystem.Interfaces
{
    public interface ICustomer
    {
        Task<IEnumerable<CustomerDto>> GetCustomers(CancellationToken cancellationToken);
        Task<CustomerDto?> GetCustomer(int id, CancellationToken cancellationToken);
        Task<int> CreateCustomer(CustomerRequest customerRequest);
    }
}
