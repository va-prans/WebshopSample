﻿using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entities.IntermediaryTables;

namespace Webshop.Domain.Entities
{
    public class Cart
    {
        public int CardId { get; set; }
        public ICollection<CartItem> Items { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}