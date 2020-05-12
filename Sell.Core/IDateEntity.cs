using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core
{
    public interface IDateEntity
    {
        DateTime CreateOn { get; set; }
        DateTime UpdateOn { get; set; }
    }
}
