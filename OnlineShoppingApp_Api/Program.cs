using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp_Api.IServices;
using OnlineShoppingApp_Api.Models;
using OnlineShoppingApp_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Enable Swagger ALWAYS (not only in Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShopping API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();