using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public Country Country { get; set; }
    }
}
