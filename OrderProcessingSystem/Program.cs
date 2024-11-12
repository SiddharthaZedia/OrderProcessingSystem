using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Domain.Mapper;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Middleware;
using OrderProcessingSystem.Repositories;
using OrderProcessingSystem.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomer,CustomerService>();
builder.Services.AddScoped<IOrder,OdrerService>();
builder.Services.AddScoped<IProduct,ProductService>();
builder.Services.AddDbContext<OrderPrcossingDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddSwaggerGen();
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DomainProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.Run();
