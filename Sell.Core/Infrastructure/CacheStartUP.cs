using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sell.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Infrastructure
{
    public class CacheStartUP : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void Configure(IApplicationBuilder app)
        {


        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
                option.InstanceName = "master";
            });
            //services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<ICacheManager, RedisCacheManager>();
        }
    }
}
