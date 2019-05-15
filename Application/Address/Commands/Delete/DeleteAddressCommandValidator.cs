using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Account.Commands.Delete;

namespace Webshop.Application.Address.Commands.Delete
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty();
        }
    }
}
