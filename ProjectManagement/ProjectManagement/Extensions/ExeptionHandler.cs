using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using ProjectManagement.Infrastracture.Exceptions;
using System;

namespace ProjectManagement.Extensions
{
    public class GlobalExeptionHandleMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExeptionHandleMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExeptionHandleMiddleware(
            RequestDelegate next, 
            ILogger<GlobalExeptionHandleMiddleware> 
            logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await HandleAsync(context);
        }

        public async Task HandleAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch(CustomValidationException ex)
            {
                _logger.LogError(ex,
                    "An unhandled exception occurred: {Message}",
                    ex.Message);

                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unhandled exception occurred: {Message}",
                    ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new { message = "Internal Server Error" };
            await context.Response.WriteAsJsonAsync(response);
        }
        
        private static async Task HandleExceptionAsync(HttpContext context, CustomValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new { message = exception.Message, error = exception.Errors };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
