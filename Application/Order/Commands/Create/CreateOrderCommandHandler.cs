using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var p = await _context.Accounts
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
                        CartItems = entity.Cart.CartItems.Select(x => new CartItem() { Item = x.Item, ItemFk = x.ItemFk, CartItemId = x.CartItemId }).ToList(),
                        AccountId = entity.Cart.AccountId
                    },
                    Orders = entity.Orders.Select(x => new Domain.Entities.Order()
                    {
                        OrderId = x.OrderId,
                        AccountId = x.AccountId,                      
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
            if (p == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            if (p.Cart.CartItems.Count >= 1)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var order = _context.Orders.Add(new Domain.Entities.Order()
                        {
                            AccountId = p.AccountId,
                            IsFinalized = false,
                            IsShipped = false,
                            OrderItems = new List<OrderItem>()
                        });
                        _context.SaveChanges();
                        foreach (var item in p.Cart.CartItems)
                        {
                            order.Entity.OrderItems.Add(new OrderItem()
                            {
                                ItemFk = item.Item.ItemId,

                                OrderFk = order.Entity.OrderId
                            });
                            _context.SaveChanges();
                        }

                        if (_context.CartItems.Count(x => x.CartFk == p.Cart.CartId) > 0)
                        {
                            _context.CartItems.RemoveRange(p.Cart.CartItems);
                        }

                        //calc totals
                        if (p.Address == null)
                        {
                            throw new NotFoundException(nameof(Domain.Entities.Address), request.AccountId);
                        }
                        order.Entity.ShippingCost = (decimal) p.Address.Country.ShippingCost;
                        
                        order.Entity.TotalCost = p.Cart.CartItems.Sum(x => x.Item.Price);
                        order.Entity.Invoice = new Invoice()
                        {
                            Amount = order.Entity.ShippingCost + order.Entity.TotalCost,
                            Currency = "DKK",
                            IsPaid = false,
                            OrderId = order.Entity.OrderId
                        };

                        _context.SaveChanges();
                        transaction.Commit();
                        Domain.Entities.Order o = new Domain.Entities.Order()
                        { 
                            OrderId = order.Entity.OrderId,
                            AccountId = order.Entity.AccountId,
                            Invoice = new Invoice()
                            {
                                InvoiceId = order.Entity.Invoice.InvoiceId,
                                Amount = order.Entity.Invoice.Amount,
                                Currency = order.Entity.Invoice.Currency,
                                IsPaid = order.Entity.Invoice.IsPaid,
                                OrderId = order.Entity.Invoice.OrderId
                            },
                            IsFinalized = order.Entity.IsFinalized,
                            IsShipped = order.Entity.IsShipped,
                            ShippingCost = order.Entity.ShippingCost,
                            TotalCost = order.Entity.TotalCost,
                            TrackingId = null,
                            OrderItems = p.Cart.CartItems.Select(x => new OrderItem()
                            {
                                Item = new Domain.Entities.Item()
                                {
                                    ItemId = x.Item.ItemId,
                                    Name = x.Item.Name,
                                    Description = x.Item.Description,
                                    Price = x.Item.Price,
                                    Stock = x.Item.Stock
                                },
                                ItemFk = x.ItemFk,                              
                            }).ToList()
                        };
                        return o;
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
