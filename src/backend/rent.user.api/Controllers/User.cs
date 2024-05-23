using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson),statusCode:StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] ResquestRegisterUserJson request)
        {
            return Created();
        }
    }
}
