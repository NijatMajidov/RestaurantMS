//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;

//namespace RMS.Core.GlobalException
//{
    
//        public class GlobalExceptionMiddleware
//        {
//            private readonly RequestDelegate _next;
//            private readonly ILogger<GlobalExceptionMiddleware> _logger;
//            private readonly IWebHostEnvironment _env;

//            public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
//            {
//                _next = next;
//                _logger = logger;
//                _env = env;
//            }

//            public async Task InvokeAsync(HttpContext httpContext, IViewRenderService viewRenderService)
//            {
//                try
//                {
//                    await _next(httpContext);
//                }
//                catch (GlobalAppException ex)
//                {
//                    _logger.LogError($"A known error occurred: {ex.Message}");
//                    await HandleGlobalAppExceptionAsync(httpContext, ex, viewRenderService);
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError($"An unexpected error occurred: {ex}");
//                    await HandleExceptionAsync(httpContext, ex, viewRenderService);
//                }
//            }

//            private Task HandleGlobalAppExceptionAsync(HttpContext context, GlobalAppException exception, IViewRenderService viewRenderService)
//            {
//                context.Response.ContentType = "text/html";
//                return viewRenderService.RenderViewAsync(context, "Error/CustomError", exception.Message);
//            }

//            private Task HandleExceptionAsync(HttpContext context, Exception exception, IViewRenderService viewRenderService)
//            {
//                context.Response.ContentType = "text/html";
//                if (_env.IsDevelopment())
//                {
//                    return viewRenderService.RenderViewAsync(context, "Error/DevelopmentError", exception);
//                }
//                else
//                {
//                    return viewRenderService.RenderViewAsync(context, "Error/Error", "An unexpected error occurred.");
//                }
//            }
//        }
//    }

//}
//}
