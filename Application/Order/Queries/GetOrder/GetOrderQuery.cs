using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Order.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Domain.Entities.Order>
    {
        public int OrderId { get; set; }
    }
}
