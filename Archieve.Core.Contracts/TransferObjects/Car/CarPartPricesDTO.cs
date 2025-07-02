using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarPartPricesDTO
    {
        public string PartName { get; set; } = string.Empty;

        public int CarPartId { get; set; }

        public decimal CurrentCost { get; set; }

    }
}
