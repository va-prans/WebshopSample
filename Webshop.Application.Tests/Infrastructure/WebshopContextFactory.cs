using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Webshop.Persistence;

namespace Webshop.Application.Tests.Infrastructure
{
    public class WebshopContextFactory
    {
        public static WebshopContext Create()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new WebshopContext(options);

            context.Database.EnsureCreated();          
            return context;
        }

        public static void Destroy(WebshopContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
