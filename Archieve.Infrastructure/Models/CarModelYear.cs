using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class CarModelYear
{
    public int Id { get; set; }

    public int CarModelId { get; set; }

    public int Year { get; set; }

    public virtual CarModel CarModel { get; set; } = null!;

    public virtual ICollection<CarPartPrice> CarPartPrices { get; set; } = new List<CarPartPrice>();
}
