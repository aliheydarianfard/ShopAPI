using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Inventory
{
   public class InventoryDTO:BaseEntityDTO
    {
        public int InventoryCount { get; set; }
        public DateTime InventoryDate { get; set; }
        public string InventoryDesc { get; set; }
        public  int ProductID { get; set; }

     
    }
}
