using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Address.Commands.Create
{
    public class CreateAddressCommand : IRequest<Domain.Entities.Address>
    {
        public int AccountId { get; set; }
        public int CountryId { get; set; }
        public string Street { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
    }
}
