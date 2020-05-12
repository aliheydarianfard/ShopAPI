using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sell.Core.Infrastructure;
using Sell.Services.Catalog;
using Sell.Services.Catalog.Category;
using Sell.Services.Catalog.Inventory;
using Sell.Services.Catalog.Invoice;
using Sell.Services.Catalog.Picture;
using Sell.Services.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Services.Infrastructure
{
    public class CommonStatup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void Configure(IApplicationBuilder app)
        {
            //  app.UseMiddleware<MapsterConfigMiddleWare>();
        }

     

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
           services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPictureService, PictureService>();


        }
    }
}