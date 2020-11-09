using Microsoft.AspNetCore.Http;
using ShoppingBasket.Api.Middlewares.Models;
using ShoppingBasket.Core.ExceptionHandling;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingBasket.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is NotificationException)
                {
                    var baseExcept = ex as NotificationException;
                    var warningErrorModel = new WarningErrorModel { ErrorCode = baseExcept.ErrorCode, Message = ex.Message, NotifyType = baseExcept.NotifyType }.ToString();

                    await HandleExceptionAsync(httpContext, warningErrorModel, HttpStatusCode.InternalServerError);
                }
                else
                {
                    var warningErrorModel = new WarningErrorModel { ErrorCode = "500", Message = ex.Message, NotifyType = "error" }.ToString();

                    await HandleExceptionAsync(httpContext, warningErrorModel, HttpStatusCode.BadRequest);

                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string error, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(error);
        }
    }
}
