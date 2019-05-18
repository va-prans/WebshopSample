using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Address.Commands.Update;
using Webshop.Application.Cart.Commands.Add;
using Webshop.Application.Cart.Commands.Remove;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Domain.Entities;
using Webshop.Domain.Entities.IntermediaryTables;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Cart
{
    public class CartTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;

        public CartTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task AddItemToCartCommandTest()
        {
            var entityCart = new Domain.Entities.Cart();
            var entityAccount = _context.Accounts.Add(new Domain.Entities.Account
            {
                Password = "1234",
                Name = "Bob",
                Cart = entityCart
            });
            var entityItem0 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Ryzen 1800X",
                Description = "Powerful Central Processing Unit",
                Price = 1200,
                Stock = 200,
                Volume = 30
            });         
            await _context.SaveChangesAsync();

            var sut = new AddItemToCartCommandHandler(_context);
            var sutResult = sut.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem0.Entity.ItemId
            }, CancellationToken.None);

            sutResult.Result.CartItems.ShouldNotBeNull();
            sutResult.Result.CartItems.Count.ShouldBeGreaterThan(0);
            sutResult.Result.CartItems.ToList()[0].Item.Name.ShouldBe("Ryzen 1800X");
            sutResult.Result.CartItems.ToList()[0].Item.Price.ShouldBe(1200);
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ShouldNotBeNull();
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList()[0].Item.Name.ShouldBe("Ryzen 1800X");
        }

        [Fact]
        public async Task AddTwoItemsToCartCommandTest()
        {
            var entityCart = new Domain.Entities.Cart();
            var entityAccount = _context.Accounts.Add(new Domain.Entities.Account
            {
                Password = "1234",
                Name = "Bob",
                Cart = entityCart
            });
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

            var sut = new AddItemToCartCommandHandler(_context);
            var sutResult = sut.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem0.Entity.ItemId
            }, CancellationToken.None);
            var sut1 = new AddItemToCartCommandHandler(_context);
            var sutResult1 = sut1.Handle(new AddItemToCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem1.Entity.ItemId
            }, CancellationToken.None);

            sutResult1.Result.CartItems.ToList().ShouldNotBeNull();
            sutResult1.Result.CartItems.Count.ShouldBeGreaterThan(0);
            sutResult1.Result.CartItems.ToList()[0].Item.Name.ShouldBe("Ryzen 1800X");
            sutResult1.Result.CartItems.ToList()[0].Item.Price.ShouldBe(1200);
            sutResult1.Result.CartItems.ToList()[1].Item.Name.ShouldBe("Nvidia 2080TI");
            sutResult1.Result.CartItems.ToList()[1].Item.Price.ShouldBe(3000);
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList().ShouldNotBeNull();
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList()[0].Item.Name.ShouldBe("Ryzen 1800X");
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList()[1].Item.Name.ShouldBe("Nvidia 2080TI");
        }

        [Fact]
        public async Task RemoveItemFromCartCommandTest()
        {          
            var entityAccount = _context.Accounts.Add(new Domain.Entities.Account
            {
                Password = "1234",
                Name = "Bob",
            });
            await _context.SaveChangesAsync();
            var entityCart = _context.Carts.Add(new Domain.Entities.Cart()
            {
                Account = entityAccount.Entity
            });
            await _context.SaveChangesAsync();
            var entityItem0 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Ryzen 1800X",
                Description = "Powerful Central Processing Unit",
                Price = 1200,
                Stock = 200,
                Volume = 30
            });
            await _context.SaveChangesAsync();
            var entityItem1 = _context.Items.Add(new Domain.Entities.Item()
            {
                Name = "Nvidia 2080TI",
                Description = "Powerful Graphics Processing Unit",
                Price = 3000,
                Stock = 100,
                Volume = 50
            });
            await _context.SaveChangesAsync();
            entityAccount.Entity.Cart.CartItems = new List<CartItem>
            {
                new CartItem() {Cart = entityAccount.Entity.Cart, Item = entityItem0.Entity},
                new CartItem() {Cart = entityAccount.Entity.Cart, Item = entityItem1.Entity}
            };
            await _context.SaveChangesAsync();

            var sut = new RemoveItemFromCartCommandHandler(_context);
            var sutResult = sut.Handle(new RemoveItemFromCartCommand()
            {
                AccountId = entityAccount.Entity.AccountId,
                ItemId = entityItem0.Entity.ItemId
            }, CancellationToken.None);

            sutResult.Result.CartItems.ToList().ShouldNotBeNull();
            sutResult.Result.CartItems.Count.ShouldBeGreaterThan(0);
            sutResult.Result.CartItems.Count.ShouldBeLessThan(2);
            sutResult.Result.CartItems.ToList()[0].Item.Name.ShouldBe("Nvidia 2080TI");
            sutResult.Result.CartItems.ToList()[0].Item.Price.ShouldBe(3000);           
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList().ShouldNotBeNull();
            _context.Accounts.Find(entityAccount.Entity.AccountId).Cart.CartItems.ToList()[0].Item.Name.ShouldBe("Nvidia 2080TI");
        }
    }
}
