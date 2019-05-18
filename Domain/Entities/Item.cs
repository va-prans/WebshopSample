using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Domain.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
