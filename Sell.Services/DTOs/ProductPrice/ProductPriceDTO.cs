using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.ProductPrice
{
   public class ProductPriceDTO:BaseEntityDTO
    {
        public long ProductPricePurch { get; set; }
        public long ProductPriceSell { get; set; }
        public DateTime ProductPriceDate { get; set; }
        public string ProductPricedesc { get; set; }
        public int ProductID { get; set; }
    }
}
