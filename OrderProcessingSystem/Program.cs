using AspNetCoreRateLimit;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderProcessingSystem.Domain.Mapper;
using OrderProcessingSystem.Interfaces;
using OrderProcessingSystem.Middleware;
using OrderProcessingSystem.Repositories;
using OrderProcessingSystem.Services;
using System.Text;

string endpointUrl = "https://www.financialservice.com/stockdata";
string soapEnvelope = @"<?xml version='1.0' encoding='utf-8'?>
         <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:web='http://www.financialservice.com/web'>
            <soapenv:Header/>
            <soapenv:Body>
               <web:GetStockPrice>
                  <web:StockSymbol>MSFT</web:StockSymbol>
               </web:GetStockPrice>
            </soapenv:Body>
         </soapenv:Envelope>";
var input = new List<string> { "2010/02/20", "2 016p 19p 12", "11-18-2012", "2018 12 24", "20130720" };
DateTransform.TransformDateFormat(input).ForEach(Console.WriteLine);
HttpClient client = new HttpClient();
var request = new HttpRequestMessage(HttpMethod.Post, endpointUrl)
{
    Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml")
};

// Send the SOAP request
HttpResponseMessage response = client.SendAsync(request).Result;

// Read and parse the response
string responseContent = response.Content.ReadAsStringAsync().Result;
Console.WriteLine(responseContent);

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // For IP-based rate limiting
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IOrder, OdrerService>();
builder.Services.AddScoped<IProduct, ProductService>();

builder.Services.AddDbContext<OrderProcessingDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
    o.Providers.Add<GzipCompressionProvider>();
});
//builder.Services.AddOptions();
builder.Services.AddMemoryCache();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
//{
//    o.Authority = "";
//    o.Audience = "";
//});
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).
 AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
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
    app.UseDeveloperExceptionPage();
}

app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
app.Run();
