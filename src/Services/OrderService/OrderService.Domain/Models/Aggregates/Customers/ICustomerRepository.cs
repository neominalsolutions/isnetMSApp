using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models.Aggregates.Customers
{
  // customer nesnesi veritabanı kayıt işlemleri için Repository Pattern
  public interface ICustomerRepository:IRepository<Customer>
  {
  }
}
