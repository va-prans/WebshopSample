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
                .SingleOrDefaultAsync(c => c.AccountId == request.AccountId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Address), request.AccountId);
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
            return new Domain.Entities.Address
            {
                AccountId = entity.AccountId,
                AddressId = entity.AddressId,
                City = entity.City,
                Street = entity.Street,
                PostNumber = entity.PostNumber,
                Country = new Domain.Entities.Country
                {
                    CountryId = entity.Country.CountryId,
                    Name = entity.Country.Name,
                    IsShippable = entity.Country.IsShippable,
                    ShippingCost = entity.Country.ShippingCost
                }             
            };
        }
    }
}
