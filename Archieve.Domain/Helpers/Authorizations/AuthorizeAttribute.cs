using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Archieve.Domain.Helpers.Authorizations
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (Users)context.HttpContext.Items["User"];
            if(user == null)
            {
                context.Result = new JsonResult(new {message = "Unauthorized"})
                { StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}
