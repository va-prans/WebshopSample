using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Common;
using Webshop.Application.Exceptions;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Account.Commands.Delete
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, CommandSuccess>
    {
        private readonly IWebshopContext _context;

        public DeleteAccountCommandHandler(
            IWebshopContext context)
        {
            _context = context;
        }

        public async Task<CommandSuccess> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts
                .SingleOrDefaultAsync(c => c.AccountId == request.AccountId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new CommandSuccess() { ResourceId = request.AccountId, Success = true};
        }
    }
}
