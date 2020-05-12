using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sell.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.Infrastructure
{
    class SwagerStartUp : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.High;

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "ورژن یک",
                    Title = " وب سرویس فروشگاه",
                    Description = " در این وب سرویس مشخصات فروشگاه شرح داده می شود",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Alihf Team",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/devsharp"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://devsharp.ir/license"),
                    }
                });
            });
        }
    }
}
