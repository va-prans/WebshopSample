using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.Application.Exceptions;
using Webshop.Persistence;

namespace Webshop.Application.Country.Queries.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<Domain.Entities.Country>>
    {
        private readonly WebshopContext _context;


        public GetCountriesQueryHandler(
            WebshopContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var entityCountries = await _context.Countries.ToListAsync(cancellationToken);
            if (entityCountries == null)
            {
                throw new NotFoundException(nameof(List<Domain.Entities.Country>), 0);
            }

            return entityCountries;
        }
    }
}
