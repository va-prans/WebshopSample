using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Exceptions;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Account.Commands.Update
{

    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Domain.Entities.Account>
    {
        private readonly IWebshopContext _context;

        public UpdateAccountCommandHandler(
            IWebshopContext context)
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
            return entity;
        }
    }

}
