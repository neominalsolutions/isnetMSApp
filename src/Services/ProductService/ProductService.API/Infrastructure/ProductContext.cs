using Microsoft.EntityFrameworkCore;

namespace ProductService.Api.Infrastructure
{
  public class ProductContext:DbContext
  {

    public ProductContext(DbContextOptions<ProductContext> options):base(options)
    {

    }
  }
}
