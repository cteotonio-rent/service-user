using Microsoft.AspNetCore.Mvc;
using rent.api.Attributes;
using rent.api.Filters;
using rent.application.UseCases.User.Profile;
using rent.application.UseCases.User.Register;
using rent.application.UseCases.User.Update;
using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.api.Controllers
{
    [TypeFilter(typeof(ExceptionFilter))]
    public class UserController : RentBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), statusCode: StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), statusCode: StatusCodes.Status200OK)]
        [AuthenticateUser]
        public async Task<IActionResult> GetUserProfile(
            [FromServices] IGetUserProfileUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
        [AuthenticateUser]
        public async Task<IActionResult> Put(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}
