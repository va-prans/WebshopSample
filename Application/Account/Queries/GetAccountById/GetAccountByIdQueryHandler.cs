using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Account.Queries.GetAccountById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Domain.Entities.Account>
    {
        private readonly WebshopContext _context;

        public GetAccountByIdQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {

            var entity = await _context.Accounts
                .FindAsync(request.AccountId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }

            return new Domain.Entities.Account
            {
                AccountId = entity.AccountId,
                Name = entity.Name,                
            };
        }
    }
}
