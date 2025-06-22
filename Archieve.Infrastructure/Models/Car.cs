using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class Car
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
}
