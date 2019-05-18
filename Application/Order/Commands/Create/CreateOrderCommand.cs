using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Order.Commands.Create
{
    public class CreateOrderCommand : IRequest<Domain.Entities.Order>
    {
        public int AccountId { get; set; }
    }
}
