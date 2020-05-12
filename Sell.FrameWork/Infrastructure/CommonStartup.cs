
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sell.Core.Infrastructure;
using Shop.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.FrameWork.Infrastructure
{
    public class CommonStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void Configure(IApplicationBuilder app)
        {
            IWebHostEnvironment env = app.ApplicationServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(app =>
                {
                    app.UseMiddleware<ErrorHandlerMiddleware>();
                });
                app.UseHsts();

            }
            
    
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IErrorHandler, ErrorHandler>();
          
        }
    }
}
