using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Address.Commands.Create
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.PostNumber).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
        }
    }
}
