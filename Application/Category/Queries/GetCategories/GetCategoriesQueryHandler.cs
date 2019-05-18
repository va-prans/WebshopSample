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

namespace Webshop.Application.Category.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Domain.Entities.Category>>
    {
        private readonly WebshopContext _context;

        public GetCategoriesQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {

            List<Domain.Entities.Category> entity = await _context.Categories.ToListAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), 0);
            }

            return entity;
        }

    }
}
