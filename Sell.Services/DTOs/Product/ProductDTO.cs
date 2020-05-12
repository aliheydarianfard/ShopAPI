using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Product
{
    public class ProductDTO : BaseEntityDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductLastPrice { get; set; }
        public long ProductLastPourchFee { get; set; }
        public int ProductLastSuply { get; set; }
        public byte ProductActive { get; set; }
        public DateTime ProductStartTime { get; set; }
        public int CategoryID { get; set; }
    }
}
