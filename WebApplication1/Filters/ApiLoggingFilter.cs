using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // executa antes da Action
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("###################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // executa depois da Action
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("###################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
           _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
        }
    }
}