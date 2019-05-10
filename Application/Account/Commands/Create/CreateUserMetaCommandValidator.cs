using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Account.Commands.Create
{
    public class CreateUserMetaCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateUserMetaCommandValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(1);          
        }
    }
}
