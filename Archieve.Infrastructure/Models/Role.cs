using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class Role
{
    public int Id { get; set; }

    public Guid Uid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime DateCreated { get; set; }

    public int Status { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
