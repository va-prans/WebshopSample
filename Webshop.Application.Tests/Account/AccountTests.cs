using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Account.Commands.Delete;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Account.Queries.GetAccountById;
using Webshop.Application.Tests.Infrastructure;
using Webshop.Persistence;
using Xunit;

namespace Webshop.Application.Tests.Account
{
    [Collection("QueryCollection")]
    public class AccountTests : IClassFixture<QueryTestFixture>
    {
        private readonly WebshopContext _context;
        public AccountTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;          
        }

        [Fact]
        public async Task CreateAccountCommandTest()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob",
                OwnerId = "1234"
            }, CancellationToken.None);

            result.ShouldBeOfType<Domain.Entities.Account>();

            result.AccountId.ShouldNotBeNull();
            result.Name.ShouldBe("Bob");
            result.OwnerId.ShouldBe("1234");
            result.Address.ShouldNotBeNull();
            result.Address.City.ShouldBeNull();
            result.Cart.ShouldNotBeNull();
            result.Cart.CartItems.ShouldBeNull();
        }

        [Fact]
        public async Task GetAccountByIdQueryTest()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob",
                OwnerId = "1234"
            }, CancellationToken.None);

            var sut1 = new GetAccountByIdQueryHandler(_context);
            var result1 = await sut1.Handle(new GetAccountByIdQuery()
            {
                AccountId = result.AccountId,               
            }, CancellationToken.None);
            result1.Name.ShouldBe("Bob");
        }

        [Fact]
        public async Task UpdateAccountCommandTest()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob",
                OwnerId = "1234"
            }, CancellationToken.None);
          
            var sut1 = new UpdateAccountCommandHandler(_context);

            var result1 = await sut1.Handle(new UpdateAccountCommand()
            {
                AccountId = result.AccountId,
                Name = "John"
            }, CancellationToken.None);

            result.Name.ShouldBe("John");
        }

        [Fact]
        public async Task DeleteAccountCommandTest()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob",
                OwnerId = "1234"
            }, CancellationToken.None);

            var sut1 = new DeleteAccountCommandHandler(_context);

            var result1 = await sut1.Handle(new DeleteAccountCommand()
            {
                AccountId = result.AccountId,                
            }, CancellationToken.None);
            result1.Success.ShouldBe(true);
            _context.Accounts.Find(result1.ResourceId).ShouldBeNull();
        }
    }
}
