using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Account.Commands.Update;

namespace Webshop.Application.Address.Commands.Update
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CountryId).NotEmpty();              
            RuleFor(x => x.City).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Street).NotEmpty().MinimumLength(1);
            RuleFor(x => x.PostNumber).NotEmpty().MinimumLength(1);
        }
    }
}
