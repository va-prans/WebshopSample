using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Interfaces;
using Webshop.Persistence;

namespace Webshop.Application.Item.Queries.GetRecommendedItems
{
    public class GetRecommendedItemsQueryHandler : IRequestHandler<GetRecommendedItemsQuery, List<Domain.Entities.Item>>
    {
        private readonly WebshopContext _context;
        private readonly IRecommendedCategoryService _recommendedCategoryService;

        public GetRecommendedItemsQueryHandler(
            WebshopContext context,
            IRecommendedCategoryService recommendedCategoryService)
        {
            _context = context;
            _recommendedCategoryService = recommendedCategoryService;
        }

        public Task<List<Domain.Entities.Item>> Handle(GetRecommendedItemsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
