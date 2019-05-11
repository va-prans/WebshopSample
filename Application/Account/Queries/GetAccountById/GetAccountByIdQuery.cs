using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Account.Queries.GetAccountById
{
    public class GetAccountByIdQuery : IRequest<Domain.Entities.Account>
    {
        public int AccountId { get; set; }
    }
}
