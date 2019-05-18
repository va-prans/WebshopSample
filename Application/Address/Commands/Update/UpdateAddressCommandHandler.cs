using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Account.Commands.Update;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Address.Commands.Update
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Domain.Entities.Address>
    {
        private readonly WebshopContext _context;

        public UpdateAddressCommandHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Address> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Addresses
                .SingleOrDefaultAsync(c => c.AddressId == request.AddressId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Address), request.AddressId);
            }
            var entityCountry = await _context.Countries
                .SingleOrDefaultAsync(c => c.CountryId == request.CountryId, cancellationToken);
            if (entityCountry == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Country), request.CountryId);
            }
            entity.City = request.City;
            entity.Country = entityCountry;
            entity.PostNumber = request.PostNumber;
            entity.Street = request.Street;
            _context.Addresses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
