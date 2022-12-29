using MediatR;
using OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.Orders
{
  // istek sonucunda orderId dönsün, sipariş Id
  public class SubmitOrderCommand:IRequest<string>
  {
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>(); // sipariş itemları

    public string CustomerName { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Street { get; set; }

    public string CardNumber { get; set; }

    public string CardHolderName { get; set; }

    public int CardTypeId { get; set; } // master,amex

    public DateTime ValidThru { get; set; } // geçerlilik tarihi

  }
}
