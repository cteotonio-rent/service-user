using FluentValidation;
using rent.communication.Requests;
using rent.exceptions;

namespace rent.application.UseCases.Motorcycle.Update
{
    public class UpdateMotorcycleLicensePlateValidator: AbstractValidator<RequestUpdateMotorcycleLicensePlateJson>
    {
        public UpdateMotorcycleLicensePlateValidator()
        {
            RuleFor(motorcycle => motorcycle.LicensePlate).NotEmpty().WithMessage(ResourceMessagesException.LICENSE_PLATE_EMPTY);
        }
    }
}
