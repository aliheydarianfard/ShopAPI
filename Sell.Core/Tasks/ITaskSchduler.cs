using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Tasks
{
    public interface ITaskSchduler
    {
        bool IsActiveInStartup { get; set; }
        string Cron { get; set; }
        void Run();
    }

}
