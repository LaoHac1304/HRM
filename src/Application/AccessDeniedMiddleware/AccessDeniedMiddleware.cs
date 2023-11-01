using System.Net;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.AccessDeniedMiddleware;

public class AccessDeniedMiddleware
{
    private readonly RequestDelegate _next;

    public AccessDeniedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Bạn không có quyền sử dụng chức năng này");
        }
    }
}