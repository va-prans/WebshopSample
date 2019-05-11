using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Create;
using Webshop.Application.Exceptions;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Address.Commands.Create
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Domain.Entities.Address>
    {
        private readonly IWebshopContext _context;

        public CreateAddressCommandHandler(
            IWebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Address> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var entityAccount = await _context.Accounts
                .SingleOrDefaultAsync(c => c.AccountId == request.AccountId, cancellationToken);
            if (entityAccount == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Account), request.AccountId);
            }
            var entityCountry = await _context.Countries
                .SingleOrDefaultAsync(c => c.CountryId == request.CountryId, cancellationToken);
            if (entityCountry == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Country), request.CountryId);
            }
            var entity = new Domain.Entities.Address
            {
                AccountId = entityAccount.AccountId,
                Account = entityAccount,
                Country = entityCountry,
                City = request.City,
                Street = request.Street,
                PostNumber = request.PostNumber               
            };

            _context.Addresses.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
