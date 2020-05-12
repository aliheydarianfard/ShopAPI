using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core
{
    public class BaseEntity : Entity, IDateEntity
    {
        public BaseEntity()
        {
            CreateOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            UpdateOn = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }
        public int ID { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
