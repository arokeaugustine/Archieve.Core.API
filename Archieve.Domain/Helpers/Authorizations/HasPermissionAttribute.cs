
using Archieve.Core.Contracts.Enums;

namespace Archieve.Domain.Helpers.Authorizations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permissions): 
            base(permissions.ToString()){ }
    }
}
