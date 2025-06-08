using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.Roles;
using Archieve.Core.Contracts;

namespace Archieve.Domain.Interfaces
{
    public interface IRoleService
    {
        ValueTask<ResponseModel<string>> CreateRoleAsync(RolesDTO roles);
        ValueTask<List<string>> GetRolePermission(List<string> uids);
        ValueTask<List<RolesDTO>> GetRoles();
    }
}
