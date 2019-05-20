using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Webshop.Persistence.Infrastructure;

namespace Webshop.Persistence
{
    public class WebshopDbContextFactoryBase : DesignTimeDbContextFactoryBase<WebshopContext>
    {
        protected override WebshopContext CreateNewInstance(DbContextOptions<WebshopContext> options)
        {
            return new WebshopContext(options);
        }
    }
}
