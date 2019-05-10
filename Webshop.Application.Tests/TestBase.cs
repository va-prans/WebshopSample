using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Webshop.Persistence;

namespace Webshop.Application.Tests
{
    public class TestBase
    {
        public WebshopContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<WebshopContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new WebshopContext(builder.Options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
