using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Order.Commands.Pay
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Domain.Entities.Order>
    {
        private readonly WebshopContext _context;

        public PayOrderCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Order> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var entityOrder = await _context.Orders.FindAsync(request.OrderId);
            if (entityOrder == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), request.OrderId);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    entityOrder.Invoice.IsPaid = true;
                    foreach (var item in entityOrder.OrderItems)
                    {
                        item.Item.Stock--;
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                    return entityOrder;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new CreateFailureException("Order", nameof(Domain.Entities.Order), "Payment fail.");
                }
            }
        }
    }
}
