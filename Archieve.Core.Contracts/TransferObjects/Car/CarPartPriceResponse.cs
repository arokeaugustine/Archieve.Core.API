using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarPartPriceResponse
    {
        public int Id { get; set; }

        public int CarModelYearId { get; set; }
        public string PartName { get; set; } = string.Empty;

        public int CarPartId { get; set; }

        public decimal CurrentCost { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
