using ApplicationService.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APIGateway.Endpoints
{
    public static class ErrorEndpoints
    {
        public static void UseErrorEndpoint(this IEndpointRouteBuilder app)
        {
            // Handle all errors on this endpoint
            app.Map("/error", (HttpContext context) =>
            {
                Exception exception = context.Features.Get<IExceptionHandlerFeature>()!.Error;

                if (exception is ServiceException serviceException)
                {
                    return Results.Json(serviceException.Json, statusCode: serviceException.Code);
                }
                else if (exception is BadHttpRequestException badrequestException)
                {   
                    return Results.Json(new { code = badrequestException.StatusCode, message = "Bad Request" }, statusCode: badrequestException.StatusCode);
                }
                else
                {
                    return Results.Json(new { code = 500, message = "Unexpected error" }, statusCode: 500);
                }
            });
        }
    }
}
