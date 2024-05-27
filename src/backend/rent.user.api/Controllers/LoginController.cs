using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rent.user.application.UseCases.Login.DoLogin;
using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.api.Controllers
{
    public class LoginController : UserBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase, 
            [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
