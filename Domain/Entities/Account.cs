using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string OwnerId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
