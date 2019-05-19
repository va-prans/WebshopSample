using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Account.Commands.Update
{

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Domain.Entities.Account>
    {
        private readonly WebshopContext _context;

        public UpdateAccountCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts
                .SingleOrDefaultAsync(c => c.AccountId == request.AccountId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            entity.Name = request.Name;
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            Domain.Entities.Account ac = new Domain.Entities.Account
            {
                AccountId = entity.AccountId,
                Name = entity.Name,
                Address = entity.Address,
                Cart = entity.Cart,
                Orders = entity.Orders,
            };
            if(ac.Cart != null) ac.Cart.Account = null;
            if(ac.Orders != null)
                foreach (var acOrder in ac.Orders)
                {
                    acOrder.Account = null;
                }
            if(ac.Address != null) ac.Address.Account = null;
            return ac;
        }
    }

}
