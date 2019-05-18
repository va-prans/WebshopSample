using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Item.Queries.GetItemById;

namespace Webshop.Application.Order.Queries.GetOrder
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
