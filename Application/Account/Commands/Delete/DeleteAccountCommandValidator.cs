using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Account.Commands.Update;

namespace Webshop.Application.Account.Commands.Delete
{
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }        
}
