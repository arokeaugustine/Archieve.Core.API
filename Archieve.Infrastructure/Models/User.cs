using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Password { get; set; } = null!;

    public Guid Uid { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<UserPermission> UserPermissionCreatedByNavigations { get; set; } = new List<UserPermission>();

    public virtual ICollection<UserPermission> UserPermissionUsers { get; set; } = new List<UserPermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
