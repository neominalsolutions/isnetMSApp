



using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models.Aggregates.Customers;
using OrderService.Domain.Models.Aggregates.Orders;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Extensions;

namespace OrderService.Infrastructure.Contexts
{
  public class OrderContext:DbContext, IUnitOfWork
  {
    private readonly IMediator mediator;
    public OrderContext(DbContextOptions<OrderContext> dbContextOptions, IMediator mediator) :base(dbContextOptions)
    {
      this.mediator = mediator;

      //this.OrderItems.fi

     //var data = this.Orders.Where(x=> x.Id=="1355").SelectMany(x => x.OrderItems).ToList();
    }

    // sadece aggregateroot nesneleri dbset olarak yazdım çünkü diğer nesnelere repositoryler üzerinden erişemeyeceğim. root nesneler üzerinden işlem yapacağız.
    public DbSet<Order> Orders { get; set; }

    //public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }


    public async Task<int> SaveChanges(CancellationToken cancellation = default)
    {
      // elimizdeki eventleri çağır dedik.
      await mediator.DispatchDomainEventsAsync(this);
      // veritabanına kayıt öncesi event fırlat event nesnelerin statelerini single transaction olarak in-memory değiştirsin. En son hali topluca save et yani commitle
      // SaveChangesAsync transactional çalışır spexecute diye bir stroce proc ile save işlemlerinde transaction scope açar var commit rollback kendi içinde yapar.

      //using (var tra = await this.Database.BeginTransactionAsync())
      //{
      //  try
      //  {
      //    await base.SaveChangesAsync(cancellation);
      //    await tra.CommitAsync();
      //  }
      //  catch (Exception)
      //  {
      //    await tra.RollbackAsync();

      //    throw;
      //  }
      //}


      return await base.SaveChangesAsync(cancellation);
    }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      // composite key
      modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });
      //modelBuilder.Entity<ShipAddress>().HasNoKey();


      // Value Object Database Entegrasyonu
      modelBuilder.Entity<Order>().OwnsOne(x => x.ShipAddress)
              .Property(x => x.City)
              .HasColumnName("City")
              .IsRequired();

      modelBuilder.Entity<Order>().OwnsOne(x => x.ShipAddress)
              .Property(x => x.Country)
              .HasColumnName("Country")
              .IsRequired();

      modelBuilder.Entity<Order>().OwnsOne(x => x.ShipAddress)
          .Property(x => x.Street)
          .HasColumnName("Street")
          .IsRequired();

      base.OnModelCreating(modelBuilder);
    }

  }
}
