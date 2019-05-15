using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
