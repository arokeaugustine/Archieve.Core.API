using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class CarPartPrice
{
    public int Id { get; set; }

    public int CarModelYearId { get; set; }

    public int CarPartId { get; set; }

    public decimal CurrentCost { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public virtual CarModelYear CarModelYear { get; set; } = null!;

    public virtual CarPart CarPart { get; set; } = null!;
}
