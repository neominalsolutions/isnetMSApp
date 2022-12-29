
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Domain.Models.Aggregates.Customers;
using OrderService.Domain.Models.Aggregates.Orders;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Contexts;
using OrderService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// servis registration yapalým

builder.Services.AddPersistenceRegistration(builder.Configuration); // dbContext register
builder.Services.AddApplicationRegistration(typeof(Program));
// mediatR Api Projesinde Program class olduðu için register et.


builder.Services.AddCap(options =>
{
  options.UseEntityFramework<OrderContext>();
  options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbConnectionString"));


  options.UseDashboard(p => p.PathMatch = "/cap");

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
