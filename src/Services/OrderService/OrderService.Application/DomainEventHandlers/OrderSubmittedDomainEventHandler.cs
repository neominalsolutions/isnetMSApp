using MediatR;
using OrderService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DomainEventHandlers
{
  // IntegrationEventHandler olsaydı Masstransit IConsume interface kullanılacaktı. fakat bu bir Domain event Handler olduğu için MediatR INotificationHandler kullanıcağız
  public class OrderSubmittedDomainEventHandler : INotificationHandler<OrderSubmitted>
  {
    public Task Handle(OrderSubmitted notification, CancellationToken cancellationToken)
    {

      // MS düzeyinde iş akışı
      // Customer var mı yok mu  yoksa customer oluştur
      // Customer Payment Method var mı yok mu yoksa payment method oluştur
      // order nesnesinde customerId alanını set et
      // order nesnesinde paymentMethodId alanını set et

      // MS Scope dışındaki MS arası iş akışımız

      // git service Bus bağlan ve Product Stock değişimi için integration event publish et.


      throw new NotImplementedException();
    }
  }
}
