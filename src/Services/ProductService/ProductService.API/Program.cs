using Microsoft.EntityFrameworkCore;
using ProductService.Api.Infrastructure;
using ProductService.API.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ProductContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbConnectionString"));
  opt.EnableSensitiveDataLogging();
});


builder.Services.AddCap(options =>
{
  options.UseEntityFramework<ProductContext>();
  options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbConnectionString"));

  options.UseRabbitMQ(options =>
  {
    options.ConnectionFactoryOptions = options =>
    {
      options.Ssl.Enabled = false;
      options.HostName = "localhost";
      options.UserName = "guest";
      options.Password = "guest";
      options.Port = 5672;
    };
  });
});

/// <summary>
/// servisin net core tarafýndan dinlenmesi için açtýk
/// </summary>
builder.Services.AddTransient<OrderCreatedIntegrationEventHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
