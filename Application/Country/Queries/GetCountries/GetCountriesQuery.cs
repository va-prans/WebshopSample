using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Country.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<List<Domain.Entities.Country>>
    {
        
    }
}
