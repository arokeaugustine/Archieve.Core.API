using Archieve.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult ReturnResponse(dynamic response)
        {
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 401)
            {
                return Unauthorized(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else if (response.StatusCode == 500)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
            else
            {
                return BadRequest(response);
            }


        }



        protected IActionResult ReturnResponse(ResponseModel<T> response)
        {
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else if (response.StatusCode == 401)
            {
                return Unauthorized(response);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            else if (response.StatusCode == 500)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
            else
            {
                return BadRequest(response);
            }


        }
    }
}
