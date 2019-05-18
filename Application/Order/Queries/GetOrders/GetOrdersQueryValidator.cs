using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Webshop.Application.Order.Queries.GetOrders
{
    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}
