using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string ExInvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool IsPaid { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
