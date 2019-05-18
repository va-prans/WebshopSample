using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Order.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<Domain.Entities.Order>>
    {
        private readonly WebshopContext _context;

        public GetOrdersQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var entityOrders = await _context.Orders.Where(x => x.AccountId == request.AccountId)
                .ToListAsync(cancellationToken);
            if (entityOrders == null)
            {
                throw new NotFoundException(nameof(List<Domain.Entities.Order>), request.AccountId);
            }

            return entityOrders;
        }
    }
}
