using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Customer
{
    public class CustomerItemFilterDTO:BaseDTO
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public byte? CustomerActive { get; set; }
    }
}
