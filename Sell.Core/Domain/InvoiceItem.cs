using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class InvoiceItem:BaseEntity
    {
        public int InvoceItemCount { get; set; }
        public long InvoceItemFeeSell { get; set; }
        public long InvoceItemFeePurche { get; set; }
        public virtual int InvoiceID { get; set; }
        public virtual int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
