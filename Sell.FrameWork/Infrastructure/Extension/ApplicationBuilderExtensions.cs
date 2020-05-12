
using Microsoft.AspNetCore.Builder;
using Sell.Core.Extension;
using Sell.Core.Infrastructure;
using Sell.Core.Tasks;
using Sell.Service.Catalog.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sell.FrameWork.Infrastructure.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            var ListTypes = typeof(IApplicationStartup).GetAllClassTypes();


            List<IApplicationStartup> ListObjects = new List<IApplicationStartup>();

            foreach (var Typeitem in ListTypes)
            {
                var ob = Activator.CreateInstance(Typeitem) as IApplicationStartup;
                ListObjects.Add(ob);
            }

            foreach (var item in ListObjects)
            {
                item.Configure(application);
            }
            var ListTaskTypes = typeof(ITaskSchduler).GetAllClassTypes();
            foreach (var typeTask in ListTaskTypes)
            {
                var task = application.ApplicationServices.GetService(typeof(StockQuantityTask)) as ITaskSchduler;
                if (task.IsActiveInStartup)
                    task.ExecuteTask();

            }
        }
    }

}
