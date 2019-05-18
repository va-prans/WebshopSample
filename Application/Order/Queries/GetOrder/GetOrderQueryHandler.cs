using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Exceptions;
using Webshop.Application.Item.Queries.GetItemById;
using Webshop.Persistence;

namespace Webshop.Application.Order.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Domain.Entities.Order>
    {
        private readonly WebshopContext _context;

        public GetOrderQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var entityOrder = await _context.Orders.FindAsync(request.OrderId);
            if (entityOrder == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), request.OrderId);
            }

            return entityOrder;
        }
    }
}
