using Archieve.Core.Contracts.Enums;
using Archieve.Core.Contracts.TransferObjects.Car;
using Archieve.Domain.Helpers.Authorizations;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpPost("new")]
        public async Task<IActionResult> CreateCar([FromBody]CarRequest carRequest)
        {
            var response =  await _carService.CreateCar(carRequest);
            return ReturnResponse(response);
        }

        [HttpPost("model-new")]
        public async Task<IActionResult> CreateCarModel([FromBody]CarModelRequest model)
        {
            var response = await _carService.CreateCarModel(model);
            return ReturnResponse(response);
        }

        [HttpPost("modelyear-new")]
        public async Task<IActionResult> CreateCarYear([FromBody] CarModelYearRequest model)
        {
            var response = await _carService.CreateCarModelYear(model);
            return ReturnResponse(response);
        }


        [HttpPost("part-new")]
        public async Task<IActionResult> CreateCarPart([FromBody] CarPartRequest model)
        {
            var response = await _carService.CreateCarPart(model);
            return ReturnResponse(response);
        }


        [HttpPost("part-price-new")]
        public async Task<IActionResult> CreateCarPartPrice([FromBody] CarPartPriceRequest model)
        {
            var response = await _carService.CreateCarPartPrice(model);
            return ReturnResponse(response);
        }

        [HttpGet("get")]
        [HasPermission(Permissions.CanViewCars)]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.RetrieveCars();
            return ReturnResponse(response);
        }


        [HttpGet("get{Uid}")]
        public async Task<IActionResult> GetCar(Guid Uid)
        {
            var response = await _carService.RetrieveCar(Uid);
            return  ReturnResponse(response);
        }

        [HttpGet("parts-get")]
        public async Task<IActionResult> GetCarParts()
        {
            var response = await _carService.RetrieveCarParts();
            return ReturnResponse(response);
        }

        [HttpGet("parts-prices{Uid}")]
        public async Task<IActionResult> GetCarwithPartsPrices(Guid Uid)
        {
            var response = await _carService.RetrieveCarWithPartPrices(Uid);
            return ReturnResponse(response);
        }

        [HttpGet("parts-prices")]
        public async Task<IActionResult> GetCarswithPartsPrices()
        {
            var response = await _carService.RetrieveCarsWithPartPrices();
            return  ReturnResponse(response);
        }

        [HttpGet("by-car-year")]
        public async Task<IActionResult> GetCarByCarYear(int carModelYearId)
        {
            var response = await _carService.RetrieveCarByModelYear(carModelYearId);
            return ReturnResponse(response);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateCar(CarResponse car)
        {
            var response = await _carService.UpdateCar(car);
            return  ReturnResponse(response);
        }


        [HttpPut("part-price-update")]
        public async Task<IActionResult> UpdateCarPartPrice(CarPartPriceResponse carPartPrice)
        {
            var response = await _carService.UpdateCarPrice(carPartPrice);
            return ReturnResponse(response);
        }









    }
}
