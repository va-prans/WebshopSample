using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Country.Queries.GetCountries;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Country
{
    public class CountryTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;

        public CountryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetCountriesQueryTest()
        {
            var entityCountry1 = _context.Countries.Add(new Domain.Entities.Country()
            {
                IsShippable = true,
                Name = "Denmark",
                ShippingCost = 20
            });
            var entityCountry2 = _context.Countries.Add(new Domain.Entities.Country()
            {
                IsShippable = false,
                Name = "Sweden",
                ShippingCost = 0
            });
            await _context.SaveChangesAsync(CancellationToken.None);

            var sut = new GetCountriesQueryHandler(_context);

            var result = await sut.Handle(new GetCountriesQuery(), CancellationToken.None);
            result.Count.ShouldBe(2);
            result[0].Name.ShouldBe("Denmark");
            result[0].IsShippable.ShouldBe(true);
            result[0].ShippingCost.ShouldBe(20);
            result[1].Name.ShouldBe("Sweden");
            result[1].IsShippable.ShouldBe(false);
            result[1].ShippingCost.ShouldBe(0);
        }
    }
}
