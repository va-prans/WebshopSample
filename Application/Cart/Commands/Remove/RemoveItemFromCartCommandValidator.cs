using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Cart.Commands.Add;

namespace Webshop.Application.Cart.Commands.Remove
{
    public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
    {
        public RemoveItemFromCartCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ItemId).NotEmpty();
        }
    }
}
