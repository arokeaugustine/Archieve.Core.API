using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Archieve.Domain.Helpers.Authorizations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permissions) 
            : base(permissions.ToString()){ }
    }
}
