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
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
            && x.ModelName.ToLower() == newCarModel.ModelName.ToLower());

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
                    Message = "duplicate data. Car part already exists."
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


        public async Task<ResponseModel> CreateCarPartPrice(CarPartPriceRequest carPartPrice)
        {
            if (carPartPrice == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid data!"
                };
            }
            var checkCarModelYear = await _context.CarModelYears.AnyAsync(x => x.Id == carPartPrice.CarModelYearId);
            if (!checkCarModelYear)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid car model year"
                };
            }

            var checkCarPart = await _context.CarParts.AnyAsync(x => x.Id == carPartPrice.CarPartId);

            if (!checkCarPart)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Invalid car part"
                };
            }


            var newCarPartPrice = new CarPartPrice
            {
                CarPartId = carPartPrice.CarPartId,
                CarModelYearId = carPartPrice.CarModelYearId,
                CurrentCost = carPartPrice.CurrentCost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            };


            var checkDuplicate = await _context.CarPartPrices.AnyAsync(x =>
                x.CarPartId == newCarPartPrice.CarPartId
                && x.CarModelYearId == carPartPrice.CarModelYearId);

            if (checkDuplicate)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Duplicate data. price for this part already exist."
                };
            }

            await _context.CarPartPrices.AddAsync(newCarPartPrice);

            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "car price added successfully."
                };

            }

            return new ResponseModel
            {
                IsSuccessful = true,
                Message = "part price added successfully."
            };

        }


        public async ValueTask<ResponseModel<List<CarResponse>>> RetrieveCars()
        {
            var cars = await _context.Cars
                .Include(c => c.CarModels)
                    .ThenInclude(m => m.CarModelYears)
                    .Select(c => new CarResponse
                    {
                        Id = c.Id,
                        Uid = c.Uid,
                        Name = c.Name,
                        CarModels = c.CarModels.Select(m => new CarModelResponse
                        {
                            CarId = c.Id,
                            Id = m.Id,
                            ModelName = m.ModelName,
                            CarModelYears = m.CarModelYears.Select(y => new CarModelYearResponse
                            {
                                CarModelId = m.Id,
                                Id = y.Id,
                                Year = y.Year,
                            }).ToList()
                        }).ToList()
                    }).ToListAsync();

            if (!cars.Any())
            {
                return new ResponseModel<List<CarResponse>>
                {
                    IsSuccessful = false,
                    Message = "No cars found"
                };
            }
            return new ResponseModel<List<CarResponse>>
            {
                IsSuccessful = true,
                Message = "Cars retrieved successfully",
                Data = cars
            };
        }


        public async ValueTask<ResponseModel<CarResponse>> RetrieveCar(Guid Uid)
        {
            if (Uid == Guid.Empty)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid car Id"
                };

            }

            var car = await _context.Cars
                .Where(x => x.Uid == Uid)
                .Include(c => c.CarModels)
                    .ThenInclude(m => m.CarModelYears)
                    .Select(c => new CarResponse
                    {
                        Id = c.Id,
                        Uid = c.Uid,
                        Name = c.Name,
                        CarModels = c.CarModels.Select(m => new CarModelResponse
                        {
                            CarId = c.Id,
                            Id = m.Id,
                            ModelName = m.ModelName,
                            CarModelYears = m.CarModelYears.Select(y => new CarModelYearResponse
                            {
                                CarModelId = m.Id,
                                Id = y.Id,
                                Year = y.Year,
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefaultAsync();

            if (car == null)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "No car found"
                };
            }
            return new ResponseModel<CarResponse>
            {
                IsSuccessful = true,
                Message = "Cars retrieved successfully",
                Data = car
            };
        }


        public async ValueTask<ResponseModel<List<CarPartResponse>>> RetrieveCarParts()
        {
            var carParts = await _context.CarParts.Select(x => new CarPartResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            if (!carParts.Any())
            {
                return new ResponseModel<List<CarPartResponse>>
                {
                    IsSuccessful = false,
                    Message = "No car part found",
                    Data = new List<CarPartResponse>()
                };

            }

            return new ResponseModel<List<CarPartResponse>>
            {
                IsSuccessful = true,
                Message = "Car parts retrieved successfully",
                Data = carParts
            };
        }

        public async Task<ResponseModel<List<CarResponse>>> RetrieveCarsWithPartPrices()
        {
            var cars = await _context.Cars
                .Include(c => c.CarModels)
                    .ThenInclude(m => m.CarModelYears)
                    .Select(c => new CarResponse
                    {
                        Id = c.Id,
                        Uid = c.Uid,
                        Name = c.Name,
                        CarModels = c.CarModels.Select(m => new CarModelResponse
                        {
                            CarId = c.Id,
                            Id = m.Id,
                            ModelName = m.ModelName,
                            CarModelYears = m.CarModelYears.Select(y => new CarModelYearResponse
                            {
                                CarModelId = m.Id,
                                Id = y.Id,
                                Year = y.Year,
                                CarPartPrices = y.CarPartPrices.Select(z => new CarPartPriceResponse
                                {
                                    Id = z.Id,
                                    CarModelYearId = z.CarModelYearId,
                                    PartName = z.CarPart.Name,
                                    CarPartId = z.CarPartId,
                                    CurrentCost = z.CurrentCost,
                                    DateCreated = z.DateCreated,
                                    DateModified = z.DateModified
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToListAsync();

            if (!cars.Any())
            {
                return new ResponseModel<List<CarResponse>>
                {
                    IsSuccessful = false,
                    Message = "No cars found"
                };
            }
            return new ResponseModel<List<CarResponse>>
            {
                IsSuccessful = true,
                Message = "Cars retrieved successfully",
                Data = cars
            };

        }


        public async Task<ResponseModel<CarResponse>> RetrieveCarWithPartPrices(Guid Uid)
        {

            if (Uid == Guid.Empty)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid car Id"
                };

            }

            var car = await _context.Cars
                .Where(b => b.Uid == Uid)
                .Include(c => c.CarModels)
                    .ThenInclude(m => m.CarModelYears)
                    .Select(c => new CarResponse
                    {
                        Id = c.Id,
                        Uid = c.Uid,
                        Name = c.Name,
                        CarModels = c.CarModels.Select(m => new CarModelResponse
                        {
                            CarId = c.Id,
                            Id = m.Id,
                            ModelName = m.ModelName,
                            CarModelYears = m.CarModelYears.Select(y => new CarModelYearResponse
                            {
                                CarModelId = m.Id,
                                Id = y.Id,
                                Year = y.Year,
                                CarPartPrices = y.CarPartPrices.Select(z => new CarPartPriceResponse
                                {
                                    Id = z.Id,
                                    CarModelYearId = z.CarModelYearId,
                                    PartName = z.CarPart.Name,
                                    CarPartId = z.CarPartId,
                                    CurrentCost = z.CurrentCost,
                                    DateCreated = z.DateCreated,
                                    DateModified = z.DateModified
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefaultAsync();

            if (car == null)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "No cars found"
                };
            }
            return new ResponseModel<CarResponse>
            {
                IsSuccessful = true,
                Message = "Cars retrieved successfully",
                Data = car
            };

        }


        public async Task<ResponseModel<CarDTO>> RetrieveCarByModelYear(int modelYearId)
        {
            var modelYear = await _context.CarModelYears
                .Include(y => y.CarModel)
                    .ThenInclude(m => m.Car)
                .Include(y => y.CarPartPrices)
                    .ThenInclude(p => p.CarPart)
                .FirstOrDefaultAsync(y => y.Id == modelYearId);

            if (modelYear == null)
            {
                return new ResponseModel<CarDTO>
                {
                    IsSuccessful = false,
                    Message = "No model year found for the given ID"
                };
            }

            var car = new CarDTO
            {
                Uid = modelYear.CarModel.Car.Uid,
                Name = modelYear.CarModel.Car.Name,
                Id = modelYear.CarModel.Car.Id,
                CarModel = new CarModelDTO
                {
                    Id = modelYear.CarModelId,
                    ModelName = modelYear.CarModel.ModelName,
                    CarModelYears = new CarModelYearDTO
                    {
                        Id = modelYear.Id,
                        Year = modelYear.Year,
                        Prices = modelYear.CarPartPrices.Select(p => new CarPartPricesDTO
                        {
                            PartName = p.CarPart.Name,
                            CarPartId = p.CarPartId,
                            CurrentCost = p.CurrentCost
                        }).ToList()
                    }
                }
            };
            return new ResponseModel<CarDTO>
            {
                IsSuccessful = true,
                Message = "Car retrieved successfully",
                Data = car
            };

        }


        public async Task<ResponseModel<CarResponse>> UpdateCar(CarResponse car)
        {
            if (car == null)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid data",
                    Data = car
                };
            }

            if (string.IsNullOrWhiteSpace(car.Name))
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "Kindly provide car name",
                    Data = car
                };
            }

            var carToUpdate = await _context.Cars.FirstOrDefaultAsync(x => x.Uid == car.Uid);
            if (carToUpdate == null)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid Id",
                    Data = car
                };
            }

            carToUpdate.Name = car.Name;
            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel<CarResponse>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to update car"
                };

            }

            return new ResponseModel<CarResponse>
            {
                IsSuccessful = true,
                Message = "car updated successfully."
            };

        }

        public async Task<ResponseModel<CarPartPriceResponse>> UpdateCarPrice(CarPartPriceResponse carPartPrice)
        {
            if (carPartPrice == null)
            {
                return new ResponseModel<CarPartPriceResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid data",
                    Data = carPartPrice
                };
            }

            if (string.IsNullOrWhiteSpace(carPartPrice.PartName))
            {
                return new ResponseModel<CarPartPriceResponse>
                {
                    IsSuccessful = false,
                    Message = "Invalid data, name of car part must be specified",
                    Data = carPartPrice
                };
            }

            var carPartToUpdate = await _context.CarPartPrices
                                .FirstOrDefaultAsync(x => x.Id == carPartPrice.Id
                                && x.CarModelYearId == carPartPrice.CarModelYearId);
            if (carPartToUpdate == null)
            {
                return new ResponseModel<CarPartPriceResponse>
                {
                    IsSuccessful = false,
                    Message = "Unable to update car part price.",
                    Data = carPartPrice
                };
            }

            carPartToUpdate.CurrentCost = carPartPrice.CurrentCost;
            carPartToUpdate.DateModified = DateTime.Now;

            var save = await _context.SaveChangesAsync();

            if (save <= 0)
            {
                return new ResponseModel<CarPartPriceResponse>
                {
                    IsSuccessful = false,
                    Message = "car price added successfully."
                };

            }

            return new ResponseModel<CarPartPriceResponse>
            {
                IsSuccessful = true,
                Message = "part price added successfully."
            };


        }




    }


}
