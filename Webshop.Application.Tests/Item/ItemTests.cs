using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Item
{
    public class ItemTests : IClassFixture<QueryTestFixture>
    {

        private readonly WebshopContext _context;

        public ItemTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetItemByIdQueryTest()
        {
            var entityItem = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Ryzen 1800X",
                Description = "Powerful Central Processing Unit",
                Price = 1200,
                Stock = 200,
                Volume = 30
            });
            await _context.SaveChangesAsync(CancellationToken.None);

            var sut = await _context.Items.FindAsync(entityItem.Entity.ItemId);
            sut.Name.ShouldBe("Ryzen 1800X");
            sut.Price.ShouldBe(1200);
        }
    }
}
