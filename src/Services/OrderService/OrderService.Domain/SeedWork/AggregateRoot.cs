using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{
  // kapsayıcıdır. Bir sınıfın parçalarını tek bir küme altında ele alıp işleyebilmemizi sağlar

  // order customer gibi entityler bu sınıftan kalıtım alıcak
  // Domain Driven Design da iş süreçleri aggregate root olan sınıflardan yapılır.
  // Bu sınıf içerisinde bir olay gerçekleştiğinde bu gerçekleşen olay başka bir sınıfı ilgilendiriyorsa bu durumda domain eventler vasıtası ile loose coupling olarak birbirleri ile iletişime geçerler.

  // AggregateRoot dan türeyen sınıflar tüm iş süreçlerini kendi üzerinde tutma ve yönteme eğilimden dolayı tercih edilir. Örneğin OrderItem Order Nesnesi için bir sub class olması itibari ile order üzerinden yönetmi daha doğru olur.

  // AggregateRoot olan sınıflar içerisinde bir çok method ile davranış gösterirler
  public abstract class AggregateRoot:IAggregateRoot
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }

    // Domain Eventleri bu sınıf üzerinden fırlatırız. Bunun için MediatR paketini yükledik
    // başka aggregate root ile haberleşmemizi sağlayacak olan yapı.
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;

    // Domain Event ekleme işini sadece AddDomainEvent Methodu yapsın
    public void AddDomainEvent(INotification @event)
    {
      _domainEvents = _domainEvents ?? new List<INotification>();
      _domainEvents.Add(@event);
    }

    public void RemoveDomainEvent(INotification @event)
    {
      _domainEvents?.Remove(@event);
    }

    public void ClearDomainEvents()
    {
      _domainEvents?.Clear();
    }

  }
}
