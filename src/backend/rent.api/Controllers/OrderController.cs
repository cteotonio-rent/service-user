using Microsoft.AspNetCore.Mvc;
using rent.api.Attributes;
using rent.application.UseCases.Order.AcceptOrder;
using rent.application.UseCases.Order.DeliverOrder;
using rent.application.UseCases.Order.GetOrderDeliverMan;
using rent.application.UseCases.Order.Register;
using rent.communication.Requests;
using rent.communication.Responses;

namespace rent.api.Controllers
{
    public class OrderController : RentBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredOrderJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticateUser]
        public async Task<IActionResult> RegisterOrder(
            [FromServices] IRegisterOrderUseCase _registerOrderUseCase,
            [FromBody] RequestRegisterOrderJson request)
        {

            var response = await _registerOrderUseCase.Execute(request);
            return Created("", response);

        }

        [HttpGet(template: "{orderid}/notifieddeliveryperson")]
        [ProducesResponseType(typeof(ResponseGetOrderDeliveryPersonJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AuthenticateUser]
        public async Task<IActionResult> GetOrderDeliverMan(
            [FromServices] IGetOrderDeliveryPersonUseCase _useCase,
            [FromRoute] string orderid)
        {
            var response = await _useCase.Execute(orderid);
            if (response is null)
                return NoContent();
            return Ok(response);
        }

        [HttpPost("accept")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticateUser]
        public async Task<IActionResult> AcceptOrder(
            [FromServices] IAcceptOrderUseCase _useCase,
            [FromBody] RequestAcceptOrderJson request)
        {
            await _useCase.Execute(request);
            return NoContent();
        }

        [HttpPost("deliver")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticateUser]
        public async Task<IActionResult> DeliverOrder(
            [FromServices] IDeliverOrderUseCase _useCase,
            [FromBody] RequestDeliverOrderJson request)
        {
            await _useCase.Execute(request);
            return NoContent();
        }
    }
}
