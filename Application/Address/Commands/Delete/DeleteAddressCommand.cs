using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Webshop.Application.Common;

namespace Webshop.Application.Address.Commands.Delete
{
    public class DeleteAddressCommand : IRequest<CommandSuccess>
    {
        public int AddressId { get; set; }
    }
}
