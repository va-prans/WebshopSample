using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
        public Image Image { get; set; }
    }
}
