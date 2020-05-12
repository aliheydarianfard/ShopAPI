using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Inventory
{
    public class InventoryItemDTO:BaseItemDTO
    {
        public string ProductName { get; set; }
        public int InventoryCount { get; set; }
        public DateTime InventoryDate { get; set; }
        public string InventoryDesc { get; set; }
        public int ProductID { get; set; }
    }
}
