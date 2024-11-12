using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.DTO;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomer _customerService;
    public CustomersController(ICustomer customer)
    {
        _customerService = customer;
    }

    [HttpGet]
    public async Task<ActionResult> GetCustomers(CancellationToken cancellationToken)
    {
        return Ok(await _customerService.GetCustomers(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomer(id, cancellationToken);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
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
