using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Webshop.Application.Account.Queries.GetAccountById;

namespace Webshop.Application.Category.Queries.GetCategoryItems
{
    public class GetCategoryItemsQueryValidator : AbstractValidator<GetCategoryItemsQuery>
    {
        public GetCategoryItemsQueryValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.Desc).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }

}
