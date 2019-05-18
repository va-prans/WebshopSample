using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Exceptions;
using Webshop.Domain.Entities;
using Webshop.Domain.Entities.IntermediaryTables;
using Webshop.Persistence;

namespace Webshop.Application.Order.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Domain.Entities.Order>
    {

        private readonly WebshopContext _context;

        public CreateOrderCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var p = await _context.Accounts.FindAsync(request.AccountId);
            if (p == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            var entityCart = p.Cart;
            if (entityCart.CartItems.Count >= 1)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (p.Orders == null) p.Orders = new List<Domain.Entities.Order>();
                        var order = _context.Orders.Add(new Domain.Entities.Order()
                        {
                            Account = p,
                            IsFinalized = false,
                            IsShipped = false,
                            OrderItems = new List<OrderItem>()
                        });
                        _context.SaveChanges();
                        foreach (var item in entityCart.CartItems)
                        {
                            order.Entity.OrderItems.Add(new OrderItem()
                            {
                                Item = item.Item,
                                Order = order.Entity
                            });
                        }
                        _context.CartItems.RemoveRange(entityCart.CartItems);
                        //calc totals
                        order.Entity.ShippingCost = (decimal) p.Address.Country.ShippingCost;
                        order.Entity.TotalCost = order.Entity.OrderItems.Sum(x => x.Item.Price);
                        order.Entity.Invoice = new Invoice()
                        {
                            Amount = order.Entity.ShippingCost + order.Entity.TotalCost,
                            Currency = "DKK",
                            IsPaid = false,
                            Order = order.Entity
                        };
                        _context.SaveChanges();
                        transaction.Commit();
                        return order.Entity;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        throw new CreateFailureException("Order", nameof(Domain.Entities.Order), "TX fail.");
                    }
                }
            }
            throw new CreateFailureException("Order", nameof(Domain.Entities.Order), "No items in cart.");
        }
    }
}
