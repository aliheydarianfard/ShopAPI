
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Framework.Infrastructure
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IErrorHandler handler)
        {
            var exceptionHandler = httpContext.Features.Get<IExceptionHandlerFeature>();

            handler.GetError(exceptionHandler.Error);

            httpContext.Response.StatusCode = handler.StatusCode;
            await httpContext.Response.WriteAsync(httpContext.ToString());

        }
    }

}
