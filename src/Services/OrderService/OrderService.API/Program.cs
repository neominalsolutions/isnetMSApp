
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

var assm = Assembly.GetExecutingAssembly();

builder.Services.AddMediatR(assm);


//builder.Services.AddDbContext<OrderContext>(opt =>
//{
//  opt.UseSqlServer(builder.Configuration["OrderDbConnectionString"]);
//  opt.EnableSensitiveDataLogging();
//});

//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//var optionsBuilder = new DbContextOptionsBuilder<OrderContext>()
//    .UseSqlServer(builder.Configuration["OrderDbConnectionString"]);



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
