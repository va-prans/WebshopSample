using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Cart.Commands.Add
{
    public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
    {
        public AddItemToCartCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ItemId).NotEmpty();            
        }
    }  
}
