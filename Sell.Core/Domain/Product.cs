using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductLastPrice { get; set; }
        public long ProductLastPourchFee { get; set; }
        public int ProductLastSuply { get; set; }
        public DateTime ProductStartTime { get; set; }
        public byte ProductActive { get; set; }
        public virtual int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual List<ProductPrice> ProductPrices { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
        public virtual List<InvoiceItem> InvoiceItems { get; set; }
    }
}
