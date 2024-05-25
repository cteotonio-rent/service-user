using Microsoft.AspNetCore.Mvc;
using rent.user.api.Filters;
using rent.user.application.UseCases.User.Register;
using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson),statusCode:StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(
            [FromServices] IRegisterUserUseCase useCase, 
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty,response);
        }
    }
}
