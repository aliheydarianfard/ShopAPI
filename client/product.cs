using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class product
    {
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string LocalCreateOn { get; set; }
        public string LocalUpdateOn { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long ProductLastPrice { get; set; }
        public long ProductLastPourchFee { get; set; }
        public int ProductLastSuply { get; set; }
        public string ProductStartTime { get; set; }
        public byte ProductActive { get; set; }
        public string ProductActivePersian { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
    }
}
