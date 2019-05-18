using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Account.Commands.Update;

namespace Webshop.Application.Account.Queries.GetAccountById
{
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
    {
        public GetAccountByIdQueryValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }         
}
