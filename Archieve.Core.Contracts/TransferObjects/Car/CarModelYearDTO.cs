using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelYearDTO
    {
        public int Id { get; set; }

       // public int CarModelId { get; set; }

        public int Year { get; set; }

      //  public virtual CarModelDTO CarModel { get; set; } = null!;
        public virtual ICollection<CarPartPricesDTO> Prices { get; set; } = new List<CarPartPricesDTO>();


    }
}
