using Microsoft.AspNetCore.Builder;
using TakeJobOffer.Application.Middleware;

namespace TakeJobOffer.Domain.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder appBuilder) =>
            appBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}
