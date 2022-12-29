using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Commands.Orders;
using OrderService.Domain.Models.Aggregates.Orders;

namespace OrderService.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  // Presentation katmanının mantığı controllera gönderilen dtoları alıp uygulama içerisinde application katmanına aktarmak.
  public class OrdersController : ControllerBase
  {

    private readonly IOrderRepository orderRepository;
    private readonly IMediator mediator; // gelen iş isteklerinin Mediator tasarım deseni ile yönlendirilmesini sağlıyor

    //private SubmitOrderCommandHandler commandHandler = new SubmitOrderCommandHandler();

    public OrdersController(IOrderRepository orderRepository, IMediator mediator)
    {
      this.orderRepository = orderRepository;
      this.mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrderCommand request)
    {

      // application katmanında olması gereken kod.

      // OrderCanceled

      // OrderSubmit Use Case 
      // Bu örnek üzerinde Application Katamanı seviyesindeki aslında Presentation katmanına verilmiştir.

      //var order = new Order("mustafa", "ist", "tr", "barbaros", "123456789", DateTime.Now, "mustafa.tan", 1);
      //order.AddItem("kazak", "1", quantity: 5, 150);
      //order.AddItem("gömlek", "2", quantity: 5, 50);

      //await this.orderRepository.AddAsync(order);
      //await this.orderRepository.unitOfWork.SaveChanges();

      // orderCreated


      // application katmanı olmadan işlem yapalım

      // mediator da komut gönderirken send event fırlatırken publish methodu kullanırız

      //var @command = new SubmitOrderCommand();
      var result = await this.mediator.Send(request);

      //await commandHandler.Handle(request);


       return Ok("İşlem Başarılı");
    }


  }
}
