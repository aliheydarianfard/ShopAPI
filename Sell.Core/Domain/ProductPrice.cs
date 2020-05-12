using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
   public class ProductPrice:BaseEntity
    {
        public long ProductPricePurch { get; set; }
        public long ProductPriceSell { get; set; }
        public DateTime ProductPriceDate { get; set; }
        public string ProductPricedesc { get; set; }
        public virtual int ProductID { get; set; }
        public virtual Product product { get; set; }
    }
}
