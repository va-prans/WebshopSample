using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities.IntermediaryTables
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public Order Order { get; set; }
        public int OrderFk { get; set; }
        
        public Item Item { get; set; }
        public int ItemFk { get; set; }
    }
}
