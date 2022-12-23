using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.SeedWork
{
  public interface IAggregateRoot
  {
     string Id { get; set; }
     DateTime CreatedAt { get; set; }


    IReadOnlyCollection<INotification> DomainEvents { get; }
    void AddDomainEvent(INotification @event);

    void RemoveDomainEvent(INotification @event);


    void ClearDomainEvents();
  }
}
