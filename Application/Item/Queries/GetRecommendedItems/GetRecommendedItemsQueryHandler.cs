using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Exceptions;
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

        public async Task<List<Domain.Entities.Item>> Handle(GetRecommendedItemsQuery request, CancellationToken cancellationToken)
        {
            var itemCat = _context.Categories.FindAsync(request.CategoryId);
            if (itemCat == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.CategoryId);
            }

            var recommendedCat = await _recommendedCategoryService.GetRecommendedCategories(itemCat.Result.Name);
            List < Domain.Entities.Item > items = new List<Domain.Entities.Item>();
            foreach (var cat in recommendedCat)
            {
                var cat1 = await _context.Categories.Where(x => x.Name == cat.name).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                var item = await _context.Items.Where(x => x.CategoryId == cat1.CategoryId)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                items.Add(item);
            }

            return items;
        }
    }
}
