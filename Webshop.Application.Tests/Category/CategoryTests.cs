using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Category.Queries.GetCategories;
using Webshop.Application.Category.Queries.GetCategoryItems;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Category
{
    public class CategoryTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;

        public CategoryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetCategoriesQueryTest()
        {
            for (int i = 0; i < 20; i++)
            {
                _context.Categories.Add(new Domain.Entities.Category()
                {
                    Name = "Category" + i
                });
                await _context.SaveChangesAsync(CancellationToken.None);
            }

            var sut = new GetCategoriesQueryHandler(_context);

            var result = sut.Handle(new GetCategoriesQuery(), CancellationToken.None);
            result.Result.Count.ShouldBe(20);
        }

        [Fact]
        public async Task GetCategoryItemsQueryTest()
        {
            var entityCat = _context.Categories.Add(new Domain.Entities.Category()
            {
                Name = "Motherboards"
            });
            await _context.SaveChangesAsync();
            for (int i = 0; i < 30; i++)
            {
                _context.Items.Add(new Domain.Entities.Item()
                {
                    Category = entityCat.Entity,
                    Name = "Ryzen 1" + i + "00X",
                    Description = "Powerful CPU",
                    Stock = 50,
                    Price = 3000/(i+1)
                });
            }
            await _context.SaveChangesAsync();

            var sut = new GetCategoryItemsQueryHandler(_context);

            var result = sut.Handle(new GetCategoryItemsQuery()
            {
                CategoryId = entityCat.Entity.CategoryId,
                Desc = true,
                Page = 1,
                PageSize = 20
            }, CancellationToken.None);
            result.Result.Count.ShouldBe(20);
            result.Result[0].Price.ShouldBeGreaterThan(result.Result[1].Price);
            result.Result[0].Price.ShouldBe(3000);
        }

    }
}
