using Microsoft.AspNetCore.Mvc;
using rent.api.Attributes;
using rent.application.UseCases.Rental.Register;
using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.api.Controllers
{
    public class RentalController : RentBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredRentalJson), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
        [AuthenticateUser]
        public async Task<IActionResult> Post(
            [FromServices] IRegisterRentalUseCase useCase,
            [FromBody] RequestRegisterRentalJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
