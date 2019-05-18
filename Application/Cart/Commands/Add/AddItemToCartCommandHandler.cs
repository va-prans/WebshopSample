using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Address.Commands.Create;
using Webshop.Application.Exceptions;
using Webshop.Domain.Entities.IntermediaryTables;
using Webshop.Persistence;

namespace Webshop.Application.Cart.Commands.Add
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Domain.Entities.Cart>
    {
        private readonly WebshopContext _context;

        public AddItemToCartCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Cart> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
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
            if (entityAccount.Cart.CartItems == null) entityAccount.Cart.CartItems = new List<CartItem>();
            entityAccount.Cart.CartItems.Add(new CartItem()
            {
                Cart = entityAccount.Cart,
                Item = entityItem
            });
            await _context.SaveChangesAsync(cancellationToken);
            return entityAccount.Cart;
        }
    }
}
