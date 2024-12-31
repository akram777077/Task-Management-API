using System;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Middlewares.Attributes;

namespace TaskMangment.Api.Middlewares;

public class ValidateIdMiddleware
{
    private readonly RequestDelegate _next;

    public ValidateIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var hasSkipValidateId = endpoint?.Metadata.GetMetadata<SkipValidateIdAttribute>() != null;
        if(!hasSkipValidateId)
        {
            if (context.Request.RouteValues.TryGetValue("id", out var idValue) && 
                int.TryParse(idValue?.ToString(), out var id) && 
                id <= 0)
            {
                var errorResponse = new ErrorResponseDto
                (
                    StatusCodes.Status400BadRequest,
                    "Invalid ID",
                    "The ID provided in the request is less than or equal to zero, which is not valid for this API.",
                    DateTime.UtcNow
                );

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorResponse);
                return;
            }
        }
        await _next(context);
    }
}

