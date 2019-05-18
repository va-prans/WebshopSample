using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Category.Queries.GetCategoryItems
{
    public class GetCategoryItemsQueryHandler : IRequestHandler<GetCategoryItemsQuery, List<Domain.Entities.Item>>
    {

        private readonly WebshopContext _context;


        public GetCategoryItemsQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }


        public async Task<List<Domain.Entities.Item>> Handle(GetCategoryItemsQuery request, CancellationToken cancellationToken)
        {
            int take = request.PageSize;
            if (request.Page < 1) request.Page = 1;          
            int skip = take * (request.Page - 1);
            List<Domain.Entities.Item> entity = null;
            if (request.Desc) entity = await _context.Items.Where(c => c.Category.CategoryId == request.CategoryId).OrderByDescending(c => c.Price).Skip(skip)
                .Take(take).ToListAsync(cancellationToken);
            else entity = await _context.Items.Where(c => c.Category.CategoryId == request.CategoryId).OrderBy(c => c.Price).Skip(skip)
                .Take(take).ToListAsync(cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(List<Domain.Entities.Item>), request.CategoryId);
            }
            return entity;
        }
    }
}
