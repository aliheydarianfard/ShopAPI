using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Customer
{
    public class CustomerItemDTO : BaseItemDTO
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerTell { get; set; }
        public string CustomerAddres { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime RegisterDate { get; set; }
        public byte CustomerActive { get; set; }
        public string CustomerActiveName { get; set; }
        
    }
}
