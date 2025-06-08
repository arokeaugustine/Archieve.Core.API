using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Permission { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public virtual Role Role { get; set; } = null!;
}
