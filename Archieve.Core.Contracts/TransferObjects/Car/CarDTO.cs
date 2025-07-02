using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid Uid { get; set; }

        public  CarModelDTO CarModel { get; set; } = new CarModelDTO();
    }
}
