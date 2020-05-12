using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsharp.Framwork.Filters
{
    public class MyAutorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string Roles { get; set; }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);

            }

            if (string.IsNullOrEmpty(Roles))
            {
                return Task.CompletedTask;
            }

            var listroles = Roles.Split(",");

            if (listroles.Count() == 0)
                return Task.CompletedTask;

            var Clientroles = context.HttpContext.User.Claims.Where(p => p.Type.Contains("role")).Select(p => p.Value).ToList();
            
            
           


            if (Clientroles.Count != 0)
            {
                if (!listroles.Any(role => Clientroles.Contains(role)))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            return Task.CompletedTask;
        }
    }

}
