using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Product
{
   public class ProductFilterDTO:BaseDTO
    {
        public string ProductName { get; set; }
        public long? ProductFromPrice { get; set; }
        public long? ProductToPrice { get; set; }
        public byte ProductActive { get; set; }
        public int? CategoryID { get; set; }
    }
}
