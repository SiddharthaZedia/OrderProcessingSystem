using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OrderProcessingSystem.Caching;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.DTO;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CustomersController : ControllerBase
{
    private readonly ICustomer _customerService;
    private readonly IMemoryCache _memoryCache;

    public CustomersController(ICustomer customer,IMemoryCache memoryCache )
    {
        _customerService = customer;
        this._memoryCache = memoryCache;
    }
    [HttpGet]
    public async Task<ActionResult> GetCustomers(CancellationToken cancellationToken)
    {
        if (!_memoryCache.TryGetValue(CacheKeys.Customer, out IEnumerable<CustomerDto> customerDto))
        {
            customerDto = await _customerService.GetCustomers(cancellationToken);
            _memoryCache.Set(CacheKeys.Customer, customerDto, DateTime.Now.AddHours(1));
        }
        return Ok(customerDto);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomer(id, cancellationToken);
        if (customer == null)
        {
            return NotFound();
        }
        return new JsonResult(customer);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer(CustomerRequest customer)
    {
        int customerId = await _customerService.CreateCustomer(customer);
        if (customerId > 0)
            return CreatedAtAction(nameof(CreateCustomer), new { id = customerId });
        return StatusCode(500);
    }
}
