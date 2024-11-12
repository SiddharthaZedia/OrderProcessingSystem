using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Model.Domain;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;
using OrderProcessingSystem.Repositories;
namespace OrderProcessingSystem.Services
{
    public class ProductService : IProduct
    {
        private readonly OrderPrcossingDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(OrderPrcossingDbContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts(CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().Select(x => _mapper.Map<ProductDto>(x)).ToListAsync(cancellationToken);
        }
        public async Task<ProductDto?> GetProduct(int id, CancellationToken cancellationToken)
        {
            var Product = await _context.Products.FindAsync(id);
            if (Product != null)
                return _mapper.Map<Product, ProductDto>(Product);
            return null;
        }
        public async Task<int> CreateProduct(ProductRequest productRequest)
        {
            var product = _mapper.Map<ProductRequest,Product>(productRequest);
            _context.Products.Add(product);
            if (await _context.SaveChangesAsync() > 0)
                return product.ProductId;
            return 0;
        }
    }
}
