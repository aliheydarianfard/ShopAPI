using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Invoice
{
    public class InvoiceItemDTO : BaseItemDTO
    {
        public string TypeName{get;set;}
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerTell { get; set; }
        public string CustomerAddres { get; set; }
        public string InvoiceDate { get; set; }
        public long InvoicePrice { get; set; }
        public long InvocePricePourche { get; set; }
        public string InvoiceDesc { get; set; }
        public byte InvoiceType { get; set; }
        public  int CustomerID { get; set; }

    }
}
