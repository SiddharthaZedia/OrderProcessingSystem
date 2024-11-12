using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrder _OrderService;
    public OrderController(IOrder Order)
    {
        _OrderService = Order;
    }

    [HttpGet]
    public async Task<ActionResult> GetOrders(CancellationToken cancellationToken)
    {
        return Ok(await _OrderService.GetOrders(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrder(int id, CancellationToken cancellationToken)
    {
        var order = await _OrderService.GetOrder(id, cancellationToken);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderRequest OrderRequest)
    {
        int orderId = await _OrderService.CreateOrder(OrderRequest);
        if (orderId > 0)
            return CreatedAtAction(nameof(GetOrder), new { id = orderId });
        return StatusCode(500);
    }
}
