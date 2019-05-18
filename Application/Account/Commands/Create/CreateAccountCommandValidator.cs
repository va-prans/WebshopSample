using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Account.Commands.Create
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(1);          
        }
    }
}
