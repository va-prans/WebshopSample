using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Cart.Commands.Add
{
    public class AddItemToCartCommand : IRequest<Domain.Entities.Cart>
    {
        public int AccountId { get; set; }
        public int ItemId { get; set; }
    }
}
