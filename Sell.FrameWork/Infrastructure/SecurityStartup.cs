
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sell.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sell.FrameWork.Infrastructure
{
    public class SecurityStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.High;

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
        }

      

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

     
    }
}
