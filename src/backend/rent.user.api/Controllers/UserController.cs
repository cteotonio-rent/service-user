using Microsoft.AspNetCore.Mvc;
using rent.user.api.Attributes;
using rent.user.api.Filters;
using rent.user.application.UseCases.User.Profile;
using rent.user.application.UseCases.User.Register;
using rent.user.application.UseCases.User.Update;
using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.api.Controllers
{
    [TypeFilter(typeof(ExceptionFilter))]
    public class UserController : UserBaseController
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
