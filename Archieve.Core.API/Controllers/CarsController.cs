using Archieve.Core.Contracts.TransferObjects.Car;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
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
            return Ok(response);
        }

        [HttpPost("model-new")]
        public async Task<IActionResult> CreateCarModel([FromBody]CarModelRequest model)
        {
            var response = await _carService.CreateCarModel(model);
            return Ok(response);
        }

        [HttpPost("modelyear-new")]
        public async Task<IActionResult> CreateCarYear([FromBody] CarModelYearRequest model)
        {
            var response = await _carService.CreateCarModelYear(model);
            return Ok(response);
        }


        [HttpPost("part-new")]
        public async Task<IActionResult> CreateCarPart([FromBody] CarPartRequest model)
        {
            var response = await _carService.CreateCarPart(model);
            return Ok(response);
        }

    }
}
