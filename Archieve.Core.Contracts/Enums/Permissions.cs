using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.Enums
{
    public enum Permissions : short
    {
        NotSet = 0,
        [Display(GroupName = "Roles", Name = "CanViewAllRoles", Description = "Can view list of all roles")]
        CanViewRoles = 0x10,
        [Display(GroupName ="Users", Name = "CanViewUsers", Description = "Can view all users")]
        CanViewUsers = 0x12,
        [Display(GroupName = "Books", Name = "CanViewBooks", Description = "Can view all Books")]
        CanViewBooks = 0x13,

    }
}
