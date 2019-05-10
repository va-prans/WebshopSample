using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Webshop.Application.Common;
using Webshop.Domain.Entities;

namespace Webshop.Application.Account.Commands.Create
{
    public class CreateAccountCommand : IRequest<Domain.Entities.Account>
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
