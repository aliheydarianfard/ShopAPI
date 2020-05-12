using Sell.Serivces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.DTOs.InvoiceItem
{
    public class InvoiceItemRegisterDTO : BaseEntityDTO
    {
        public int InvoceItemCount { get; set; }
        public  int InvoiceID { get; set; }
        public  int ProductID { get; set; }
    }
}
