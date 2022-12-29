namespace ProductService.Api.IntegrationEvents
{
  public class OrderLine
  {
    public string ProductId { get; set; }
    public int Quantity { get; set; }

  }

  public class OrderCreatedIntegrationEvent
  {
    public string OrderId { get; private set; }

    public List<OrderLine> OrderLines { get; private set; } = new List<OrderLine>();

    public OrderCreatedIntegrationEvent(string orderId,List<OrderLine> orderLines)
    {
      OrderId = orderId;
      OrderLines = orderLines;
    }
  }
}
