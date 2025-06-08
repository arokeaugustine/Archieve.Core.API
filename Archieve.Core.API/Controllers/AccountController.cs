using Archieve.Core.Contracts.TransferObjects.User;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        public AccountController(IAccountService userService)
        {
            this.service = userService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var response = await this.service.CreateUser(request);
            return Ok(response);
        }

    }
}