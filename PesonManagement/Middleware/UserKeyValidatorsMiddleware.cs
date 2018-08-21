using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Middleware
{
    public class UserKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;

        public UserKeyValidatorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                if (!context.Request.Headers.Keys.Contains("X-My-Test-Header"))
                {
                    context.Response.StatusCode = 400; //Bad Request                
                    await context.Response.WriteAsync("User Key is missing");
                    return;
                }
                else
                {
                    if (context.Request.Headers["X-My-Test-Header"].First() != "XX-Secret+xxx+xx+hambamjdo")
                    {
                        context.Response.StatusCode = 401; //UnAuthorized
                        await context.Response.WriteAsync("Invalid User Key");
                        return;
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}
