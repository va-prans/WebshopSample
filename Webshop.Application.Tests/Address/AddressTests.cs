using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Address.Commands.Create;
using Webshop.Application.Address.Commands.Delete;
using Webshop.Application.Address.Commands.Update;
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
            result.AddressId.ShouldBeGreaterThanOrEqualTo(1);
            var resultEntity = await _context.Addresses.SingleOrDefaultAsync(x => x.AccountId == result.AccountId);
            resultEntity.City.ShouldBe("Copenhagen");
            resultEntity.Country.Name.ShouldBe("Denmark");
            resultEntity.Account.Name.ShouldBe("Bob");
        }

        [Fact]
        public async Task DeleteAddressCommandTest()
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

            var entityAddress = new Domain.Entities.Address
            {
                Account = entityAccount,
                AccountId = entityAccount.AccountId,
                Country = entityCountry,
                City = "Copenhagen",
                Street = "BobStreet",
                PostNumber = "2000"
            };
            await _context.Addresses.AddAsync(entityAddress);
            await _context.SaveChangesAsync();

            var sut = new DeleteAddressCommandHandler(_context);
            var result = await sut.Handle(new DeleteAddressCommand()
            {
                AddressId = entityAddress.AddressId,               
            }, CancellationToken.None);
            result.Success.ShouldBe(true);
            _context.Addresses.Find(result.ResourceId).ShouldBeNull();

        }

        [Fact]
        public async Task UpdateAddressCommandTest()
        {
            var entityCountry = new Domain.Entities.Country
            {
                Name = "Denmark",
                ShippingCost = 20,
                IsShippable = true,
                Tax = 25
            };
            var entityCountry1 = new Domain.Entities.Country
            {
                Name = "Sweden",
                ShippingCost = 20,
                IsShippable = true,
                Tax = 25
            };
            await _context.Countries.AddAsync(entityCountry);
            await _context.Countries.AddAsync(entityCountry1);
            await _context.SaveChangesAsync();

            var entityAccount = new Domain.Entities.Account
            {
                Name = "Bob",
                OwnerId = "1234"
            };
            await _context.Accounts.AddAsync(entityAccount);
            await _context.SaveChangesAsync();

            var entityAddress = new Domain.Entities.Address
            {
                Account = entityAccount,
                AccountId = entityAccount.AccountId,
                Country = entityCountry,
                City = "Copenhagen",
                Street = "BobStreet",
                PostNumber = "2000"
            };
            await _context.Addresses.AddAsync(entityAddress);
            await _context.SaveChangesAsync();

            var sut = new UpdateAddressCommandHandler(_context);
            var result = await sut.Handle(new UpdateAddressCommand()
            {
                AddressId = entityAddress.AddressId,
                CountryId = entityCountry1.CountryId,
                City = "Malmo",
                Street = "AliceStreet",
                PostNumber = "1000"
            }, CancellationToken.None);
           
            var updatedAddress = await _context.Addresses.FindAsync(result.AddressId);
            updatedAddress.Country.Name.ShouldBe("Sweden");
            updatedAddress.City.ShouldBe("Malmo");
            updatedAddress.Street.ShouldBe("AliceStreet");
            updatedAddress.PostNumber.ShouldBe("1000");
        }
    }
}
