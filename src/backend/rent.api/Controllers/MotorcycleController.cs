using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using rent.api.Attributes;
using rent.application.UseCases.Motorcycle.Delete;
using rent.application.UseCases.Motorcycle.Get;
using rent.application.UseCases.Motorcycle.Register;
using rent.application.UseCases.Motorcycle.Update;
using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.api.Controllers
{
    public class MotorcycleController : RentBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredMotorcycleJson), statusCode: StatusCodes.Status201Created)]
        [AuthenticateUser]
        public async Task<IActionResult> Post(
            [FromServices] IRegisterMotorcycleUseCase useCase,
            [FromBody] RequestRegisterMotorcycleJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet("GetByLicensePlate")]
        [ProducesResponseType(typeof(ResponseGetMotocycle), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [AuthenticateUser]
        public async Task<IActionResult> Get(
            [FromServices] IGetMotorcycleUseCase useCase,
            [FromQuery] string licensePlate)
        {
            var response = await useCase.Execute(licensePlate);
            if (response is not null)
                return Ok(response);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [AuthenticateUser]
        public async Task<IActionResult> Put(
            [FromServices] IUpdateMotorcycleLicensePlateUseCase useCase,
            [FromRoute] string id,
            [FromBody] RequestUpdateMotorcycleLicensePlateJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [AuthenticateUser]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteMotorcycleUseCase useCase,
            [FromRoute] string id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
