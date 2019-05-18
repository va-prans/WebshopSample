using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Delete;
using Webshop.Application.Common;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Address.Commands.Delete
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, CommandSuccess>
    {
        private readonly WebshopContext _context;

        public DeleteAddressCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<CommandSuccess> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Addresses
                .SingleOrDefaultAsync(c => c.AddressId == request.AddressId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Address), request.AddressId);
            }
            _context.Addresses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new CommandSuccess() { ResourceId = request.AddressId, Success = true };
        }
    }
}
