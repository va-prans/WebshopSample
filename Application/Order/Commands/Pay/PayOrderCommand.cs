using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Order.Commands.Pay
{
    public class PayOrderCommand : IRequest<Domain.Entities.Order>
    {
        public int OrderId { get; set; }
    }
}
