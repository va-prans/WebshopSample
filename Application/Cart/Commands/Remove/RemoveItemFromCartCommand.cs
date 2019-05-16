using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Cart.Commands.Remove
{
    public class RemoveItemFromCartCommand : IRequest<Domain.Entities.Cart>
    {
        public int AccountId { get; set; }
        public int ItemId { get; set; }
    }
}
