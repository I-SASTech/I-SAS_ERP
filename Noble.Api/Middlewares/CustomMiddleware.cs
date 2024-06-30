using System;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using Focus.Business;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Noble.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IApplicationDbContext context, IPrincipal principal)
        {
            try
            {
                if (httpContext.Request.Path.Value == "/api/Sale/SaveSaleInformation" || httpContext.Request.Path.Value == "/api/Purchase/SaveSaleOrderInformation")
                {
                    if (!Requests.IsDuplicateSale)
                    {
                        Requests.IsDuplicateSale = true;
                        await _next(httpContext);
                    }

                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log issues and handle exception response

            if (exception.GetType() == typeof(ValidationException))
            {
                var code = HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(((ValidationException)exception).Failures);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(result);
                return Task.FromResult(result);
            }
            if (exception.GetType() == typeof(ObjectAlreadyExistsException))
            {
                var code = HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(((ObjectAlreadyExistsException)exception).Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(result);
                return Task.FromResult(result);
            }
            if (exception.GetType() == typeof(NotFoundException))
            {
                var code = HttpStatusCode.NotFound;
                var result = JsonConvert.SerializeObject(JsonConvert.SerializeObject(((NotFoundException)exception).Message));
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(result);
                return Task.FromResult(result);
            }


            if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                var code = HttpStatusCode.Unauthorized;
                var result = JsonConvert.SerializeObject(JsonConvert.SerializeObject(((UnauthorizedAccessException)exception).Message));
                //var result = JsonConvert.SerializeObject(new { isSuccess = false, error = exception.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(result);
                return Task.FromResult(result);
            }
            else
            {
                var code = HttpStatusCode.InternalServerError;
                var result = JsonConvert.SerializeObject(exception.Message);
                //var result = JsonConvert.SerializeObject(new { isSuccess = false, error = exception.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
