using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Car;
using Archieve.Domain.Interfaces;
using Archieve.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Domain.Services
{
    public class CarServices : ICarService
    {
        ArchieveContext _context;
        public CarServices(ArchieveContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel> CreateCar(CarRequest car)
        {
            if (car == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid Data. Name of car must be provided"
                };
            }


            if (string.IsNullOrWhiteSpace(car.Name))
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid Data. Name of car must be provided"
                };
            }
            var newCar = new Car
            {
                Name = car.Name,
            };

            var duplicate = await _context.Cars
                .AnyAsync(x => x.Name.ToLower() == car.Name.ToLower());
            
            if (duplicate)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "duplicate data! Car already exists"
                };
            }

            await _context.Cars.AddAsync(newCar);
            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "unsuccessful"
                };

            }

            return new ResponseModel
            {
                IsSuccessful = true,
                Message = "successful"
            };

        }

        public async Task<ResponseModel> CreateCarModel(CarModelRequest carModel)
        {
            if (carModel == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. Car model is required."
                };

            }

            if (carModel.CarId < 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. Car is required."
                };
            }

            if (string.IsNullOrWhiteSpace(carModel.ModelName))
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "the model of this car is required"
                };
            }

            var newCarModel = new CarModel
            {
                CarId = carModel.CarId,
                ModelName = carModel.ModelName,
            };

            var carCheck = await _context.Cars.AnyAsync(x => x.Id == newCarModel.CarId);
            if (!carCheck)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid car. Please provide an accepted car."
                };
            }

            var duplicate = await _context.CarModels.AnyAsync(x =>
            x.CarId == newCarModel.CarId
            && x.ModelName.ToLower() == newCarModel.ModelName.ToLower() );

            if (duplicate)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "duplicate data. Car model already exists."
                };
            }

            await _context.CarModels.AddAsync(newCarModel);
            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "unsuccessful"
                };

            }

            return new ResponseModel
            {
                IsSuccessful = true,
                Message = "successful"
            };

        }

        public async Task<ResponseModel> CreateCarModelYear(CarModelYearRequest carModelYear)
        {
            if (carModelYear == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. Car model is required."
                };

            }

            if (carModelYear.CarModelId < 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. Car is required."
                };
            }

            if (carModelYear.Year < 2000)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid year! the year of this car is required"
                };
            }

            var newCarModelYear = new CarModelYear
            {
                Year = carModelYear.Year,
                CarModelId = carModelYear.CarModelId,
            };



            var modelYearCheck = await _context.CarModels.AnyAsync(x => x.Id == newCarModelYear.CarModelId);
            if (!modelYearCheck)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid car model."
                };
            }

            var duplicateCheck = await _context.CarModelYears.AnyAsync(x =>
            x.CarModelId == newCarModelYear.CarModelId
            && x.Year == newCarModelYear.Year);

            if (duplicateCheck)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Duplicate data! car year already exists."
                };
            }


            await _context.CarModelYears.AddAsync(newCarModelYear);
            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "unsuccessful"
                };

            }

            return new ResponseModel
            {
                IsSuccessful = true,
                Message = "successful"
            };

        }

        public async Task<ResponseModel> CreateCarPart(CarPartRequest carPart)
        {
            if (carPart == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. Car part is required."
                };
            }

            if (string.IsNullOrWhiteSpace(carPart.Name))
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data. name of car part is required."
                };
            }

            var newCarPart = new CarPart
            {
                Name = carPart.Name
            };

            var duplicate = await _context.CarParts.AnyAsync(x =>
            x.Name.ToLower() == carPart.Name.ToLower());

            if (duplicate)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "duplicate data. Car model already exists."
                };
            }


            await _context.CarParts.AddAsync(newCarPart);

            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "unsuccessful"
                };

            }

            return new ResponseModel
            {
                IsSuccessful = true,
                Message = "successful"
            };

        }




    }
}
