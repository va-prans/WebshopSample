using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Exceptions;
using Webshop.Domain.Entities.IntermediaryTables;
using Webshop.Persistence;

namespace Webshop.Application.Account.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Domain.Entities.Account>
    {
        private readonly WebshopContext _context;

        public GetAccountByIdQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {

            var entityX = await _context.Accounts            
                .Where(x => x.AccountId == request.AccountId)
                .Select(entity => new Domain.Entities.Account
                {
                    AccountId = entity.AccountId,
                    Name = entity.Name,
                    Address = new Domain.Entities.Address()
                    {
                        Country = entity.Address.Country,
                        AccountId = entity.Address.AccountId,
                        AddressId = entity.Address.AddressId,
                        City = entity.Address.City,
                        Street = entity.Address.Street,
                        PostNumber = entity.Address.PostNumber
                    },                   
                    Cart = new Domain.Entities.Cart()
                    {
                        CartId = entity.Cart.CartId,
                        CartItems = entity.Cart.CartItems.Select(x => new CartItem() {Item = x.Item, ItemFk = x.ItemFk, CartItemId = x.CartItemId}).ToList(),
                        AccountId = entity.Cart.AccountId
                    },
                    Orders = entity.Orders.Select(x => new Domain.Entities.Order()
                    {
                        OrderId = x.OrderId,
                        AccountId = x.AccountId,
                        Invoice = x.Invoice,
                        IsFinalized = x.IsFinalized,
                        IsShipped = x.IsShipped,
                        ShippingCost = x.ShippingCost,
                        TotalCost = x.TotalCost,
                        OrderItems = x.OrderItems.Select(y => new OrderItem() { Item = y.Item, ItemFk = y.ItemFk, OrderItemId = y.OrderItemId}).ToList()
                    }).ToList()                 
                }).FirstOrDefaultAsync(cancellationToken);

            if (entityX == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }

            Domain.Entities.Account ac = new Domain.Entities.Account
            {
                AccountId = entityX.AccountId,
                Name = entityX.Name,
                Address = entityX.Address,
                Cart = entityX.Cart,
                Orders = entityX.Orders,
            };
            ac.Cart.Account = null;
            if (ac.Orders != null)
                foreach (var acOrder in ac.Orders)
                {
                    acOrder.Account = null;
                }
            ac.Address.Account = null;
            return entityX;
        }
    }
}
