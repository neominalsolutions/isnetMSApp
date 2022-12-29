using MediatR;
using OrderService.Domain.Models.Aggregates.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.Orders
{
  public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, string>
  {
    private readonly IOrderRepository orderRepository;

    public SubmitOrderCommandHandler(IOrderRepository orderRepository)
    {
      this.orderRepository = orderRepository;
    }


    public async Task<string> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
      // gelen iş istediği bu handle methodu içerisinde işlenecek.


      var order = new Order(
        customerName: request.CustomerName,
        city:request.City,
        country:request.Country,
        street:request.Street,
        cardNumber:request.CardNumber,
        cardHolderName:request.CardHolderName,
        validThru:request.ValidThru,
        cardTypeId:request.CardTypeId
        );


      foreach (var item in request.Items)
      {
        order.AddItem(
          productName: item.ProductName,
          productId: item.ProductId,
          quantity: item.Quantity,
          listPrice: item.ListPrice);
      }


      await orderRepository.AddAsync(order);
      var result =  await orderRepository.unitOfWork.SaveChanges();

      if(result == 0)
      {
        throw new Exception("Submit Order işlemi sırasında bir hata meydana geldi");
      }

      // bu süre. sonrasında biz event fırlatacağız (Integration Event) (OrderCreatedEventi olucak)


      return await Task.FromResult<string>(order.Id);
    }
  }
}
