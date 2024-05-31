using FluentValidation;
using rent.communication.Requests;
using rent.exceptions;

namespace rent.application.UseCases.Order.Register
{
    public class RegisterOrderValidator: AbstractValidator<RequestRegisterOrderJson>
    {
        public RegisterOrderValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(ResourceMessagesException.ORDER_PRICE_INVALID);
        }
    }
}
