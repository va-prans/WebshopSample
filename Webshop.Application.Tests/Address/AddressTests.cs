using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Address.Commands.Create;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Address
{
    [Collection("QueryCollection")]
    public class AddressTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;
        public AddressTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateAddressCommandTest()
        {
            var entityCountry = new Domain.Entities.Country
            { 
                Name = "Denmark",
                ShippingCost = 20,
                IsShippable = true,
                Tax = 25
            };
            await _context.Countries.AddAsync(entityCountry);
            await _context.SaveChangesAsync();

            var entityAccount = new Domain.Entities.Account
            {
                Name = "Bob",
                OwnerId = "1234"
            };
            await _context.Accounts.AddAsync(entityAccount);
            await _context.SaveChangesAsync();

            var sut = new CreateAddressCommandHandler(_context);
            var result = await sut.Handle(new CreateAddressCommand()
            {
                AccountId = entityAccount.AccountId,
                CountryId = entityCountry.CountryId,
                City = "Copenhagen",
                PostNumber = "2000",
                Street = "TigerVej 199, 3th."

            }, CancellationToken.None);
            result.AccountId.ShouldBeGreaterThanOrEqualTo(1);
            var resultEntity = await _context.Addresses.SingleOrDefaultAsync(x => x.AddressId == result.AccountId);
            resultEntity.City.ShouldBe("Copenhagen");
            resultEntity.Country.Name.ShouldBe("Denmark");
            resultEntity.Account.Name.ShouldBe("Bob");
        }
    }
}
