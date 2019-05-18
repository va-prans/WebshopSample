using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public double ShippingCost { get; set; }
        public bool IsShippable { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
