using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.Enums;

namespace Archieve.Domain.Helpers.Authorizations
{
    public static class PermissionPackers
    {
        public const char PackType = 'H';
        public const int PackSize = 4;


        public static string FormDefaultPackPrefix()
        {
            return $"{PackType}{PackSize:D1}-";
        }

        public static string packPermissionIntoString(this IEnumerable<Permissions> permissions)
        {
            return permissions.Aggregate(FormDefaultPackPrefix(), (s,permissions) => s + ((int)permissions).ToString("X4"));
        }

        public static IEnumerable<int> UnpackPermissionValuesFromString(this string packedPermission)
        {
            var packPrefix = FormDefaultPackPrefix();
            if (packedPermission == null)
            {
                throw new ArgumentNullException(nameof(packedPermission));
            }
            if (!packedPermission.StartsWith(packPrefix))
            {
                throw new InvalidOperationException("The format of the packed permission is wrong" +
                                        $" - should start with {packPrefix}");
            }

            int index = packPrefix.Length;
            while (index < packedPermission.Length)
            {
                var dd = int.Parse(packedPermission.Substring(index, PackSize), NumberStyles.HexNumber);
                yield return dd;
                index += PackSize;
            }
        }

        public static IEnumerable<Permissions> UnpackPermissionsFromString(this string packedPermissions)
        {
            return packedPermissions.UnpackPermissionValuesFromString().Select(x => ((Permissions)x));
        }

        public static Permissions? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out Permissions permission)
                ? permission : null;
        }


    }
}
