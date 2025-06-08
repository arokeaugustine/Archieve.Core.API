using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Archieve.Domain.Helpers.Authorizations
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.packedPermissionClaimType);
            if (permissionsClaim == null)
            {
                return Task.CompletedTask;
            }
            if (permissionsClaim.Value.ThisPermissionIsAllowed(requirement.Permission))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
