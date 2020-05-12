using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Domain
{
    public class Picture:BaseEntity
    {
        public string VirtualPath { get; set; }
        public string MimeType { get; set; }
        public int ProductID { get; set; }
    }
}
