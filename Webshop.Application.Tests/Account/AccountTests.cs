using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Account.Commands.Delete;
using Webshop.Application.Account.Commands.Login;
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
                Password = "1234"
            }, CancellationToken.None);

            result.ShouldBeOfType<Domain.Entities.Account>();
            result.AccountId.ShouldNotBeNull();
            result.Name.ShouldBe("Bob");
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
                Name = "Bob1",
                Password = "1234"
            }, CancellationToken.None);

            var sut1 = new GetAccountByIdQueryHandler(_context);
            var result1 = await sut1.Handle(new GetAccountByIdQuery()
            {
                AccountId = result.AccountId,               
            }, CancellationToken.None);
            result1.Name.ShouldBe("Bob1");
        }

        [Fact]
        public async Task LoginCommandTest_CorrectDetails()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob2",
                Password = "1234"
            }, CancellationToken.None);

            var sut1 = new LoginCommandHandler(_context);
            var result1 = await sut1.Handle(new LoginCommand()
            {
                Name = "Bob2",
                Password = "1234"
            }, CancellationToken.None);
            result1.Name.ShouldBe("Bob2");
        }

        [Fact]
        public async Task UpdateAccountCommandTest()
        {
            var result = _context.Accounts.Add(new Domain.Entities.Account()
            {
                Name = "Bob3",
                Password = "1234"
            });
            await _context.SaveChangesAsync();

            var sut = new UpdateAccountCommandHandler(_context);
            var sutResult = await sut.Handle(new UpdateAccountCommand()
            {
                AccountId = result.Entity.AccountId,
                Name = "John"
            }, CancellationToken.None);

            sutResult.Name.ShouldBe("John");
        }

        [Fact]
        public async Task DeleteAccountCommandTest()
        {
            var result = _context.Accounts.Add(new Domain.Entities.Account()
            {
                Name = "Bob4",
                Password = "1234"
            });
            await _context.SaveChangesAsync();

            var sut = new DeleteAccountCommandHandler(_context);
            var sutResult = await sut.Handle(new DeleteAccountCommand()
            {
                AccountId = result.Entity.AccountId,                
            }, CancellationToken.None);

            sutResult.Success.ShouldBe(true);
            _context.Accounts.Find(sutResult.ResourceId).ShouldBeNull();
        }
    }
}
