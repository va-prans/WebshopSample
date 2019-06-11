using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Item.Queries.GetItemById;

namespace Webshop.Application.Item.Queries.GetRecommendedItems
{
    public class GetRecommendedItemsQueryValidator : AbstractValidator<GetRecommendedItemsQuery>
    {
        public GetRecommendedItemsQueryValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
