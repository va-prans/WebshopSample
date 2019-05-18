using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Address.Commands.Create;
using Webshop.Application.Cart.Commands.Add;
using Webshop.Application.Order.Commands.Create;
using Webshop.Application.Order.Commands.Pay;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Order
{
    public class OrderTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;

        public OrderTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task CreateOrderCommandTest()
        {
            var entityCountry = _context.Countries.AddAsync(new Domain.Entities.Country
            {
                Name = "Denmark",
                ShippingCost = 20,
                IsShippable = true,
            });
            await _context.SaveChangesAsync();

            var entityCart = new Domain.Entities.Cart();
            var entityAccount = _context.Accounts.Add(new Domain.Entities.Account
            {
                OwnerId = "1234",
                Name = "Bob",
                Cart = entityCart
            });
            await _context.SaveChangesAsync();

            var add = new CreateAddressCommandHandler(_context);
            var addResult = await add.Handle(new CreateAddressCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                CountryId = entityCountry.Result.Entity.CountryId,
                City = "Copenhagen",
                PostNumber = "2000",
                Street = "TigerVej 199, 3th."

            }, CancellationToken.None);
            var entityItem0 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Ryzen 1800X",
                Description = "Powerful Central Processing Unit",
                Price = 1200,
                Stock = 200,
                Volume = 30
            });
            var entityItem1 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Nvidia 2080TI",
                Description = "Powerful Graphics Processing Unit",
                Price = 3000,
                Stock = 100,
                Volume = 50
            });
            await _context.SaveChangesAsync();

            var add1 = new AddItemToCartCommandHandler(_context);
            var add1Result = add1.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem0.Entity.ItemId
            }, CancellationToken.None);
            var add2 = new AddItemToCartCommandHandler(_context);
            var add2Result1 = add2.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem1.Entity.ItemId
            }, CancellationToken.None);
            //create order

            var sut = new CreateOrderCommandHandler(_context);

            var result = sut.Handle(new CreateOrderCommand()
            {
                AccountId = entityAccount.Entity.AccountId
            }, CancellationToken.None);
            result.Result.Invoice.Amount.ShouldBe(4220);
            result.Result.Invoice.IsPaid.ShouldBe(false);
            result.Result.TotalCost.ShouldBe(4200);
            result.Result.OrderItems.Count.ShouldBe(2);
            result.Result.Account.Address.Street.ShouldBe("TigerVej 199, 3th.");
            _context.Accounts.FindAsync(entityAccount.Entity.AccountId).Result.Cart.CartItems.Count.ShouldBe(0);
        }

        [Fact]
        public async Task PayOrderCommandTest()
        {
            var entityCountry = _context.Countries.AddAsync(new Domain.Entities.Country
            {
                Name = "Denmark",
                ShippingCost = 20,
                IsShippable = true,
            });
            await _context.SaveChangesAsync();

            var entityCart = new Domain.Entities.Cart();
            var entityAccount = _context.Accounts.Add(new Domain.Entities.Account
            {
                OwnerId = "1234",
                Name = "Bob",
                Cart = entityCart
            });
            await _context.SaveChangesAsync();

            var add = new CreateAddressCommandHandler(_context);
            var addResult = await add.Handle(new CreateAddressCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                CountryId = entityCountry.Result.Entity.CountryId,
                City = "Copenhagen",
                PostNumber = "2000",
                Street = "TigerVej 199, 3th."

            }, CancellationToken.None);
            var entityItem0 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Ryzen 1800X",
                Description = "Powerful Central Processing Unit",
                Price = 1200,
                Stock = 200,
                Volume = 30
            });
            var entityItem1 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Nvidia 2080TI",
                Description = "Powerful Graphics Processing Unit",
                Price = 3000,
                Stock = 100,
                Volume = 50
            });
            await _context.SaveChangesAsync();

            var add1 = new AddItemToCartCommandHandler(_context);
            var add1Result = add1.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem0.Entity.ItemId
            }, CancellationToken.None);
            var add2 = new AddItemToCartCommandHandler(_context);
            var add2Result1 = add2.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem1.Entity.ItemId
            }, CancellationToken.None);
            //create order
            var add3 = new CreateOrderCommandHandler(_context);

            var add3Result = add3.Handle(new CreateOrderCommand()
            {
                AccountId = entityAccount.Entity.AccountId
            }, CancellationToken.None);
            //pay order

            var sut = new PayOrderCommandHandler(_context);

            var result = sut.Handle(new PayOrderCommand()
            {
                OrderId = add3Result.Result.OrderId
            }, CancellationToken.None);
            result.Result.Invoice.IsPaid.ShouldBe(true);

        }
    }
}
