using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Common;
using Webshop.Application.Exceptions;
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

            var nameExists = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == request.Name);
            if (nameExists != null)
            {
                throw new CreateFailureException(nameof(Domain.Entities.Account), 0, "Name already exists.");
            }
            var entity = _context.Accounts.AddAsync(new Domain.Entities.Account
            {
                Password = request.Password,
                Name = request.Name,
            }, cancellationToken).Result.Entity;
            await _context.SaveChangesAsync(cancellationToken); 

            var entityAddress = await _context.Addresses.AddAsync(new Domain.Entities.Address {Account = entity, AccountId = entity.AccountId}, cancellationToken);
            var entityCart = await _context.Carts.AddAsync(new Domain.Entities.Cart {Account = entity, AccountId = entity.AccountId }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Domain.Entities.Account ac = new Domain.Entities.Account
            {
                AccountId = entity.AccountId,
                Name = entity.Name,
                Address = entity.Address,
                Cart = entity.Cart,
                Orders = entity.Orders,
            };
            if (ac.Cart != null) ac.Cart.Account = null;
            if (ac.Orders != null)
                foreach (var acOrder in ac.Orders)
                {
                    acOrder.Account = null;
                }
            if (ac.Address != null) ac.Address.Account = null;
            return ac;
        }
    }
}
