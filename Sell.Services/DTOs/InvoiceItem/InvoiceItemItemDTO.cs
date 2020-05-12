using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.InvoiceItem
{
   public class InvoiceItemItemDTO:BaseItemDTO
    {
        public string CustomerName { get; set; }
        public int InvoceItemCount { get; set; }
        public long FeePriceSell { get; set; } 
        public long FeePricePurche { get; set; } 
        public string ProductName { get; set; }
    }
}
