using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Product> products { get; set; }
    }
}
