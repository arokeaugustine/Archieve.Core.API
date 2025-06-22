using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class CarModel
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public string ModelName { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<CarModelYear> CarModelYears { get; set; } = new List<CarModelYear>();
}
