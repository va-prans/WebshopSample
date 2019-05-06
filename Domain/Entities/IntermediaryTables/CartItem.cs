using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities.IntermediaryTables
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public Cart Cart { get; set; }
        public int CartFk { get; set; }

        public Item Item { get; set; }
        public int ItemFk { get; set; }
    }
}
