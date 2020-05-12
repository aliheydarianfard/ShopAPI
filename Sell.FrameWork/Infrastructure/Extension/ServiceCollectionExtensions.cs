

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sell.Core.Extension;
using Sell.Core.Infrastructure;
using Sell.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sell.FrameWork.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
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
                item.ConfigureServices(services, configuration);
            }
            var ListTaskTypes = typeof(ITaskSchduler).GetAllClassTypes();
            foreach (var typeTask in ListTaskTypes)
            {
                services.AddScoped(typeTask);
            }
        }
    }
}
