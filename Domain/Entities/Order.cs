using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public Invoice Invoice { get; set; }
        public bool IsShipped { get; set; }
        public bool IsFinalized { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }
        public string TrackingId { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
