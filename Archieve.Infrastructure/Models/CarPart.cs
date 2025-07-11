﻿using System;
using System.Collections.Generic;

namespace Archieve.Infrastructure.Models;

public partial class CarPart
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CarPartPrice> CarPartPrices { get; set; } = new List<CarPartPrice>();
}
