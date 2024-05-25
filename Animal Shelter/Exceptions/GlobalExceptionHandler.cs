using Microsoft.AspNetCore.Diagnostics;

namespace Animal_Shelter.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }   
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred");
        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync("An error occurred while processing your request", cancellationToken);
        return true;
    }
}