using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<Domain.Entities.Account>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
