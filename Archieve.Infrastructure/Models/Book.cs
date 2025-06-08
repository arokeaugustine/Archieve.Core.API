using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DateDeleted { get; set; }
}
