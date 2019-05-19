using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Account.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Domain.Entities.Account>
    {
        private readonly WebshopContext _context;

        public LoginCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.Name);
            }

            if (request.Password != entity.Password)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), "Incorrect Password.");
            }
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
