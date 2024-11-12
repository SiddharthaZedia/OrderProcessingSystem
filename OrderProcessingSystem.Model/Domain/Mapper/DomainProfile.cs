using AutoMapper;
using OrderProcessingSystem.Model.Domain;
using OrderProcessingSystem.Model.DTO;
using OrderProcessingSystem.Model.Request;

namespace OrderProcessingSystem.Domain.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductRequest, Product>();
            CreateMap<OrderRequest, Order>();
            CreateMap<Order, OrderDto>();
                //.ForMember(dest => dest.we, opts => opts.MapFrom(source => source.Weight))
        }
    }
}
