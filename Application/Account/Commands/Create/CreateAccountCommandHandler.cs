using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Common;
using Webshop.Persistence;

namespace Webshop.Application.Account.Commands.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Domain.Entities.Account>
    {
        private readonly WebshopContext _context;

        public CreateAccountCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {           
            var entityAddress = new Domain.Entities.Address();
            var entityCart = new Domain.Entities.Cart();
            var entity = _context.Accounts.Add(new Domain.Entities.Account
            {
                Password = request.Password,
                Name = request.Name,
                Cart = entityCart,
                Address = entityAddress,
            });
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }
    }
}
