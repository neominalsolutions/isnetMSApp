using DotNetCore.CAP;
using ProductService.Api.IntegrationEvents;

namespace ProductService.API.IntegrationEvents
{
  public class OrderCreatedIntegrationEventHandler:ICapSubscribe
  {
    [CapSubscribe("OrderCreated")]
    public void Consume(OrderCreatedIntegrationEvent @event)
    {
      // Product Stock güncelleme işlemi
    }
  }
}
