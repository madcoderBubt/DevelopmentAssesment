using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
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
            await Handle(context);
        }

        public async Task Handle(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unhandled exception occurred: {Message}",
                    ex.Message);
            }
        }
    }
}
