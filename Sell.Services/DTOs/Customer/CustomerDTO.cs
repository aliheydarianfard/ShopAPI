using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sell.Services.DTOs.Customer
{
    public class CustomerDTO:BaseEntityDTO
    {

        public string CustomerName { get; set; }

        public string CustomerMobile { get; set; }
        public string CustomerTell { get; set; }
        public string CustomerAddres { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime RegisterDate { get; set; }
        public byte CustomerActive { get; set; }
    }
}
