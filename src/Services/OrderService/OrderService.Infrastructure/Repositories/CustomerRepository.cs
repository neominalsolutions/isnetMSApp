using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models.Aggregates.Customers;
using OrderService.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
  public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
  {
    private readonly OrderContext dbContext;
    public CustomerRepository(OrderContext dbContext) : base(dbContext)
    {
      this.dbContext = dbContext;
    }

    public override async Task<Customer> GetAsync(Expression<Func<Customer, bool>> expression)
    {
      return await this.dbContext.Customers.Include(x => x.PaymentMethods).FirstOrDefaultAsync(expression);
    }
  }


}
