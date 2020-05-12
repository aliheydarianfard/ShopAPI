

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sell.Core.Infrastructure;
using Sell.Data.Repositores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Data.InfraStructure
{
    public class DatabaseStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.AboveNormal;

        public void Configure(IApplicationBuilder app)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddDbContextPool<IApplcationDbContext, SQLServerApplicationContext>(
                       c => c.UseSqlServer("Data Source=.;Initial Catalog=SellAPI;Integrated Security=true;")
                   , poolSize: 16);

        }

       
    }
}
