using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Category.Queries.GetCategoryItems
{
    public class GetCategoryItemsQuery : IRequest<List<Domain.Entities.Item>>
    {
        public int CategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
    }
}
