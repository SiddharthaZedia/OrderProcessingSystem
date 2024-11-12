using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.Request;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProduct _ProductService;
    public ProductController(IProduct Product)
    {
        _ProductService = Product;
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts(CancellationToken cancellationToken)
    {
        return Ok(await _ProductService.GetProducts(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var product = await _ProductService.GetProduct(id, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(ProductRequest productRequest)
    {
        int productId = await _ProductService.CreateProduct(productRequest);
        if (productId > 0)
            return CreatedAtAction(nameof(GetProduct), new { id = productId });
        return StatusCode(500);
    }
}
