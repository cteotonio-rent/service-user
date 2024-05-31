using FluentValidation;
using rent.communication.Requests;
using rent.exceptions;

namespace rent.application.UseCases.Motorcycle.Register
{
    public class RegisterMotorcycleValidator: AbstractValidator<RequestRegisterMotorcycleJson>
    {
        public RegisterMotorcycleValidator()
        {
            RuleFor(motorcycle => motorcycle.Model).NotEmpty().WithMessage(ResourceMessagesException.MODEL_EMPTY);
            RuleFor(motorcycle => motorcycle.Year).NotEmpty().WithMessage(ResourceMessagesException.YEAR_EMPTY);
            RuleFor(motorcycle => motorcycle.LicensePlate).NotEmpty().WithMessage(ResourceMessagesException.LICENSE_PLATE_EMPTY);

            When(motorcycle => !string.IsNullOrEmpty(motorcycle.Year), () =>
            {
                RuleFor(user => user.Year)
                .Custom((x, context) =>
                {
                    if (x.Length != 4)
                        context.AddFailure(ResourceMessagesException.YEAR_QTDE_CHARACTERES_INVALID);
                    else if (!int.TryParse(x, out int year))
                        context.AddFailure(ResourceMessagesException.YEAR_INVALID_IS_NOT_NUMBER);
                });
            });
        }
    }
}
