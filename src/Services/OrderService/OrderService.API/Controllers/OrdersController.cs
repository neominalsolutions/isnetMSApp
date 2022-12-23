using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Models.Aggregates.Orders;

namespace OrderService.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {

    private readonly IOrderRepository orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
      this.orderRepository = orderRepository;
    }


    [HttpPost]
    public async Task<IActionResult> SubmitOrder()
    {

      var order = new Order("mustafa", "ist", "tr", "barbaros", "123456789", DateTime.Now, "mustafa.tan", 1);
      order.AddItem("kazak", "1", quantity: 5, 150);
      order.AddItem("gömlek", "2", quantity: 5, 50);

      await this.orderRepository.AddAsync(order);
      await this.orderRepository.unitOfWork.SaveChanges();


      // application katmanı olmadan işlem yapalım

       return Ok();
    }


  }
}
