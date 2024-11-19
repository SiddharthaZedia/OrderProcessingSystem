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
        private readonly OrderProcessingDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(OrderProcessingDbContext context, IMapper autoMapper)
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
            var product = await _context.Products.FindAsync(id);
            if (product != null)
                return _mapper.Map<Product, ProductDto>(product);
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

        public async Task<int> UpdateProduct(int id, UpdateRequest productRequest)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return 0;
            product = _mapper.Map<UpdateRequest, Product>(productRequest);
            _context.Entry(product).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}
