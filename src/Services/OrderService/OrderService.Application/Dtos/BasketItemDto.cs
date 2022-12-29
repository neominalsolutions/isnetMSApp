using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Dtos
{
  public class BasketItemDto
  {
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal ListPrice { get; set; }
    public string ProductName { get; set; }

  }
}
