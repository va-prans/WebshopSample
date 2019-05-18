using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Order.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<Domain.Entities.Order>>
    {
        public int AccountId { get; set; }
    }
}
