using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Webshop.Application.Address.Commands.Update
{
    public class UpdateAddressCommand : IRequest<Domain.Entities.Address>
    {
        public int AddressId { get; set; }
        public int CountryId { get; set; }
        public string Street { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
    }
}
