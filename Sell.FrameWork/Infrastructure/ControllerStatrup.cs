using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sell.Core.Infrastructure;
using Sell.Framwork.Infrastructure.ModelBinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sell.FrameWork.Infrastructure
{
    public class ControllerStatrup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Low;

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

      

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            IMvcBuilder configController = services.AddControllers(
                option => {
                    option.ModelBinderProviders.Insert(0, new PersianDateEntityBinderProvider());
                }
                );
            services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", options =>
             {
                 options.Authority = "https://localhost:6001";
                 options.RequireHttpsMetadata = false;

                 options.Audience = "SellAPI";
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = "name",
                 };
             });

            configController.ConfigureApiBehaviorOptions((option) => {

                option.ClientErrorMapping[404].Title = " منبع مورد نظر پیدا نشد ";

                option.InvalidModelStateResponseFactory = (Context) => {

                    var values = Context.ModelState.Values.Where(state => state.Errors.Count != 0)
                  .Select(state => state.Errors.Select(p => new { errorMessage = p.ErrorMessage }));

                    return new BadRequestObjectResult(values);

                };
            });
        }

       
    }
}
