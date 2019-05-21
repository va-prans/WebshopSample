using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Item.Queries.GetRecommendedItems
{
    public class GetRecommendedItemsQuery : IRequest<List<Domain.Entities.Item>>
    {
        public int CategoryId { get; set; }
    }   
}
