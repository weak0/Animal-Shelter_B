using Microsoft.AspNetCore.Diagnostics;

namespace Animal_Shelter.Exceptions;

public class AppExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public AppExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        (int statusCode, string? errorMessage) = exception switch
        {
            ForbiddenException => (403, null),
            BadRequestException badRequestException => (400, badRequestException.Message),
            NotFoundException notFoundException => (404, notFoundException.Message),
            _ => default
        };
        if (statusCode == default)
            return false;
        
        _logger.LogError(exception, exception.Message);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);
        
        return true;
    }
}