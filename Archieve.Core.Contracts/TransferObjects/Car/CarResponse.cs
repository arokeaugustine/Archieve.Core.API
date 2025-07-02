using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid Uid { get; set; }

        public virtual ICollection<CarModelResponse> CarModels { get; set; } = new List<CarModelResponse>();
    }

}
