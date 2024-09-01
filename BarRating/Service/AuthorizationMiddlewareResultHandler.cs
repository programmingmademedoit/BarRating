using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace BarRating.Service
{
        public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
        {
            public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
            {
                if (!authorizeResult.Succeeded)
                {
                    var referer = context.Request.Headers["Referer"];

                    if (!referer.Contains("?authenticate=true"))
                    {
                        context.Response.Redirect(referer + "?authenticate=true");
                        return;
                    }
                    else
                    {
                        context.Response.Redirect(referer);
                        return;
                    }
                }

                await next(context);
            }
        }
  }  
