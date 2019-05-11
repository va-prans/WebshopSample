using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Webshop.Application.Common;

namespace Webshop.Application.Account.Commands.Delete
{
    public class DeleteAccountCommand : IRequest<CommandSuccess>
    {
        public int AccountId { get; set; }
    }
}
