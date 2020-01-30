using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PingPong.Sdk;
using PingPong.Sdk.Models;

namespace PingPong.API.Filters
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            DictionaryKeyPolicy         = JsonNamingPolicy.CamelCase,
            IgnoreNullValues            = true,
            IgnoreReadOnlyProperties    = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy        = JsonNamingPolicy.CamelCase,
            WriteIndented               = false
        };
        
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                await ProcessException(context, ex);
            }
        }

        private async Task ProcessException(HttpContext context, Exception ex)
        {
            _logger.LogInformation($"Error of type {ex.GetType().Name} has been caught.");

            var error = ex switch
            {
                ApiException apiEx => apiEx.Error,
                _                  => new ErrorDto(500, "Unexpected exception", "UNKNOWN")
            };

            var errorJson = JsonSerializer.Serialize(error, _jsonOptions);
            
            context.Response.StatusCode = error.StatusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(errorJson);
            await context.Response.CompleteAsync();
        }
    }
}