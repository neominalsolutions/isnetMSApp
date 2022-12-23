using Microsoft.EntityFrameworkCore;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{

  // Generic Repository pattern
  public class GenericRepository<T> : IRepository<T> where T : AggregateRoot
  {

    private readonly OrderContext dbContext;


    public GenericRepository(OrderContext orderContext)
    {
      dbContext = orderContext;
    }

    public IUnitOfWork unitOfWork => (IUnitOfWork)dbContext;


    public virtual async Task AddAsync(T entity)
    {
      await dbContext.Set<T>().AddAsync(entity);
    }

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
       return await dbContext.Set<T>().FirstOrDefaultAsync(expression);
    }

    public virtual async Task<T> GetById(string id)
    {
      return await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
  }
}
