using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelResponse
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string ModelName { get; set; } = null!;

        public virtual CarResponse Car { get; set; } = null!;

        public virtual ICollection<CarModelYearResponse> CarModelYears { get; set; } = new List<CarModelYearResponse>();
    }
}
