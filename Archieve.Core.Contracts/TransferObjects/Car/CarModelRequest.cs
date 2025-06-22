using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Car
{
    public class CarModelRequest
    {
        public int CarId { get; set; }
        public string ModelName { get; set; } = string.Empty;   
    }
}
