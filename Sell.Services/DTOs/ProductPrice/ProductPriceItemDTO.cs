using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.ProductPrice
{
   public class ProductPriceItemDTO:BaseDTO
    {
        public string ProductName { get; set; }
        public long ProductPricePurch { get; set; }
        public long ProductPriceSell { get; set; }
        public int ProductID { get; set; }
        public string ProductPriceDate { get; set; }

    }
}
