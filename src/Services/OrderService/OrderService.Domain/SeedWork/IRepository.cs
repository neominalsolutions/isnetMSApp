using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{
  public interface IRepository<T>
  {
    IUnitOfWork unitOfWork { get; }

    Task AddAsync(T entity); // kayıt girme , delete,update
    Task<T> GetById(string id); // ilgili kaydı dbden okuma

    Task<T> GetAsync(Expression<Func<T, bool>> expression); // x=> x.customerName='mert'
  }
}
