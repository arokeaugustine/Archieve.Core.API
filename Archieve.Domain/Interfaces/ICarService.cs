using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Car;
using Archieve.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Interfaces
{
    public interface ICarService
    {
        Task<ResponseModel> CreateCar(CarRequest car);
        Task<ResponseModel> CreateCarModel(CarModelRequest carModel);
        Task<ResponseModel> CreateCarModelYear(CarModelYearRequest carModelYear);
        Task<ResponseModel> CreateCarPart(CarPartRequest carPart);

    }
}
