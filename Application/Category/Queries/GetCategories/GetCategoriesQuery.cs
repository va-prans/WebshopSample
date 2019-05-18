using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Category.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<Domain.Entities.Category>>
    {
    }    
}
