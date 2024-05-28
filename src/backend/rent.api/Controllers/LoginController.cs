using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rent.application.UseCases.Login.DoLogin;
using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.api.Controllers
{
    public class LoginController : RentBaseController
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
