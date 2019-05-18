using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Item.Queries.GetItemById
{
    public class GetItemByIdQuery : IRequest<Domain.Entities.Item>
    {
        public int ItemId { get; set; }
    }
   
}
