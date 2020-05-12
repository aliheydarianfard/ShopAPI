using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Inventory : BaseEntity
    {
        public int InventoryCount { get; set; }
        public DateTime InventoryDate { get; set; }
        public string InventoryDesc { get; set; }
        public virtual int ProductID { get; set; }
        public Product product { get; set; }
    }
}
