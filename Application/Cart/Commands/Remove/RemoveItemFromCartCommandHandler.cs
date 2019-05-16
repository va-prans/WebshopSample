using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Cart.Commands.Add;
using Webshop.Application.Exceptions;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Application.Cart.Commands.Remove
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, Domain.Entities.Cart>
    {
        private readonly IWebshopContext _context;

        public RemoveItemFromCartCommandHandler(
            IWebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Cart> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var entityAccount = await _context.Accounts
                .SingleOrDefaultAsync(c => c.AccountId == request.AccountId, cancellationToken);
            if (entityAccount == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            var entityItem = await _context.Items
                .SingleOrDefaultAsync(c => c.ItemId == request.ItemId, cancellationToken);
            if (entityItem == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Item), request.AccountId);
            }
            var items = _context.CartItems.Where(c =>
                c.CartFk == entityAccount.Cart.CartId && c.ItemFk == entityItem.ItemId).ToList();
            if (items.Count >= 1)
            {
                
                _context.CartItems.Remove(items[0]);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entityAccount.Cart;
        }
    }
}
