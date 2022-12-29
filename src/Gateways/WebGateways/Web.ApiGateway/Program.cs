using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.ConfigureAppConfiguration((host, config) =>
{
  config.SetBasePath(host.HostingEnvironment.ContentRootPath).AddJsonFile("Configurations/ocelot.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
});

builder.Services.AddOcelot(); // ocelot hizmetini service olarak sisteme ekledik.
// ocelot ile ilgiki config dosyasý içine yazýlmýþ servisleri sisteme tanýtmamýzý saðlayan registeration servis


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseOcelot(); // ocelot config üzerinden requestlerin yönlendirlmesi için yazýlmýþ bir ara yazýlým.

app.Run();
