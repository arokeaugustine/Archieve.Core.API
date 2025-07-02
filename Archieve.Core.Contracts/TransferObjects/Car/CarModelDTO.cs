using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelDTO
    {
        public int Id { get; set; }

        //public int CarId { get; set; }

        public string ModelName { get; set; } = null!;

       // public virtual CarDTO Car { get; set; } = null!;

        public CarModelYearDTO CarModelYears { get; set; } = new CarModelYearDTO();
    }
}
