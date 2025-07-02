using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelYearResponse
    {
        public int Id { get; set; }

        public int CarModelId { get; set; }

        public int Year { get; set; }

        public virtual CarModelResponse CarModel { get; set; } = null!;

       public virtual ICollection<CarPartPriceResponse> CarPartPrices { get; set; } = new List<CarPartPriceResponse>();
    }
}
