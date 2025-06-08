using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.Enums;

namespace Archieve.Domain.Helpers.Authorizations
{
    public static class PermissionChecker
    {
        public static bool ThisPermissionIsAllowed(this string packedPermission, string permissionName)
        {
            var usersPermissions = packedPermission.UnpackPermissionsFromString().ToArray();

            if(!Enum.TryParse(permissionName, true, out Permissions permissionsToCheck))
            {
                throw new Exception($"{permissionName} could not be converted to {nameof(Permissions)}.");
            }
            return usersPermissions.Contains(permissionsToCheck);
        }
    }
}
