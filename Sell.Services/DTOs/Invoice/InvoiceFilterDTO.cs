using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.Invoice
{
    public class InvoiceFilterDTO
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddres { get; set; }
        //public DateTime? FromInvoiceDate { get; set; }
        //public DateTime? ToInvoiceDate { get; set; }
        public long? FromInvoicePrice { get; set; }
        public long? ToInvoicePrice { get; set; }
        public byte InvoiceType { get; set; }

    }
}
