using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ParcelLockTest.Contract;

namespace ParcelLockTest.Api.Common;

public class HttpResponseException : Exception {
    public HttpStatusCode StatusCode { get; set; }

    public int ErrorCode { get; set; }

    public HttpResponseException(string message) : base(message) { }

    public HttpResponseException(string message, Exception innerException) : base(message, innerException) { }
}

public static class HttpResponseExceptionHandler {
    public static async Task Handle(HttpContext context, bool writeStackTrace) {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        context.Response.ContentType = "application/json";
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (ex is HttpResponseException appError) {
            context.Response.StatusCode = (int)appError.StatusCode;
            var response = appError.StatusCode switch {
                HttpStatusCode.BadRequest => new DefaultValidationResponse(),
                _ => new ErrorResponse()
            };
            response.Message = appError.Message;
            response.ErrorCode = appError.ErrorCode;
            response.StackTrace = writeStackTrace ? appError.ToString() : null;
            if (appError.StatusCode == HttpStatusCode.BadRequest) {
                await context.Response.WriteAsJsonAsync(response);
            }
        }
        else {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new ErrorResponse {
                Message = "Unhandled error",
                ErrorCode = 500,
                StackTrace = writeStackTrace ? ex?.ToString() : null
            });
        }
    }
}
