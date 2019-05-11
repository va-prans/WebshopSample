using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Account.Commands.Update
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(1);
        }
    }
}
