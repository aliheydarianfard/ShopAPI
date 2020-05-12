using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Invoice
{
    public class InvoiceDTO:BaseEntityDTO
    {
        public long InvoicePrice { get; set; }
        public long InvocePricePourche { get; set; }
        public string InvoiceDesc { get; set; }
        public byte InvoiceType { get; set; }
        public DateTime InvoiceDate { get; set; }
        public  int CustomerID { get; set; }
    }
}
