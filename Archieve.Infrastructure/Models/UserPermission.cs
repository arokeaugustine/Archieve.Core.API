using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class UserPermission
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Permission { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public int CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
