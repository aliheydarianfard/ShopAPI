using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Invoice:BaseEntity
    {
        public DateTime InvoiceDate { get; set; }
        public long InvoicePrice { get; set; }
        public long InvocePricePourche { get; set; }
        public string InvoiceDesc { get; set; }
        public byte InvoiceType { get; set; }
        public virtual int CustomerID { get; set; }
        public virtual Customer customer { get; set; }
        public virtual List<InvoiceItem> InvoiceItems { get; set; }
    }
}
