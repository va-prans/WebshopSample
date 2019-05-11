using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Account.Commands.Update
{
    public class UpdateAccountCommand : IRequest<Domain.Entities.Account>
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
    }
}
