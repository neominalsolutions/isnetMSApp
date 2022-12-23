using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{
  public interface IUnitOfWork
  {
    // kayıt Orm tool üzerinden veri tabanına yansıyacak.
    // transactional bir şekilde ilgili database nesnesine tek yerden save işlemi uygulayacağız.
    Task<int> SaveChanges(CancellationToken cancellation = default(CancellationToken));
  }
}
