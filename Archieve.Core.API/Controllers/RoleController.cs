using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Core.Contracts.TransferObjects.Roles;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService service;

        public RoleController(IRoleService service)
        {
            this.service = service;
        }

        [HttpPost("create")]
        public IActionResult Create(RolesDTO role)
        {
            var response = this.service.CreateRoleAsync(role);
            return Ok(response);
        }
    }
}
