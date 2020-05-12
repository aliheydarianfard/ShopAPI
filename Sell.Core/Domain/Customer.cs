using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Customer:BaseEntity
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerTell { get; set; }
        public string CustomerAddres { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime RegisterDate { get; set; }
        public byte CustomerActive { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}
