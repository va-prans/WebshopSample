using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public double Tax { get; set; }
        public double ShippingCost { get; set; }
        public bool IsShippable { get; set; }
        ICollection<Address> Addresses { get; set; }
    }
}
