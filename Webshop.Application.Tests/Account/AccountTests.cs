using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Webshop.Application.Account.Commands.Create;
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
        public async Task CreateAccountTest()
        {
            var sut = new CreateAccountCommandHandler(_context);

            var result = await sut.Handle(new CreateAccountCommand()
            {
                Name = "Bob",
                OwnerId = "1234"
            }, CancellationToken.None);

            result.ShouldBeOfType<Domain.Entities.Account>();

            result.Name.ShouldBe("Bob");
            result.OwnerId.ShouldBe("1234");
            result.Address.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateAccountTest()
        {
            
        }
    }
}
