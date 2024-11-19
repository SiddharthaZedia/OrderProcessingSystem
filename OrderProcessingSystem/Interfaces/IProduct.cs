using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;

namespace OrderProcessingSystem.Interfaces
{
    public interface IProduct
    {
        Task<IEnumerable<ProductDto>> GetProducts(CancellationToken cancellationToken);
        Task<ProductDto?> GetProduct(int id, CancellationToken cancellationToken);
        Task<int> CreateProduct(ProductRequest productRequest);
        Task<int> UpdateProduct(int id,UpdateRequest productRequest);
    }
}
