using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Tasks
{
    public static class TaskExtension
    {
        public static void ExecuteTask(this ITaskSchduler task)
        {
            RecurringJob.AddOrUpdate(() => task.Run(), task.Cron);
        }

    }
}
