using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Item.Queries.GetItemById
{
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Domain.Entities.Item>
    {
        private readonly WebshopContext _context;

        public GetItemByIdQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {

            var entity = await _context.Items
                .FindAsync(request.ItemId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Item), request.ItemId);
            }

            return entity;
        }
    }
}
