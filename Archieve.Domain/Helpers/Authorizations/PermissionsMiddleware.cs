using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Archieve.Domain.Helpers.Authorizations
{
    public class PermissionsMiddleware
    {

        private readonly RequestDelegate _next;

        public PermissionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var cancellationToken = context.RequestAborted;

            var userUid = context.User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userUid))
            {
                await context.WriteAccessDeniedResponse("user 'uid' claim is required", cancellationToken: cancellationToken);
                return;
            }

            await _next(context);
        }
    }
}
