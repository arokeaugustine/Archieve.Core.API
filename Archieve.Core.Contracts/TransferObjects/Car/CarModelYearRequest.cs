using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelYearRequest
    {
        public int CarModelId { get; set; }
        public int Year { get; set; }
    }
}
