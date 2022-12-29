using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ProductService.Api.Infrastructure;

namespace OrderService.Infrastructure.Context
{
    public class ProductContextDesignFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContextDesignFactory()
        {
        }

        public ProductContext CreateDbContext(string[] args)
        {
            var connStr = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=ProductTestDB;Trusted_Connection=True";
            //var connStr = "Data Source=c_sqlserver;Initial Catalog=OrderDB;Trusted_Connection=True";

          var optionsBuilder = new DbContextOptionsBuilder<ProductContext>()
                .UseSqlServer(connStr);

            return new ProductContext(optionsBuilder.Options);
        }


  }
}
