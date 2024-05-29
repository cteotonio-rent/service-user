using FluentValidation;
using rent.communication.Requests;
using rent.exceptions;

namespace rent.application.UseCases.User.Register
{
    public class RegisterUserValidator: AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_EMPTY);
            When(user => !string.IsNullOrEmpty(user.Email), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });
            RuleFor(user => user.NRLE).NotEmpty().WithMessage(ResourceMessagesException.NRLE_EMPTY);
            RuleFor(user => user.DriversLicense).NotEmpty().WithMessage(ResourceMessagesException.DRIVERS_LICENSE_EMPTY);
            RuleFor(user => user.DriversLicenseCategory).NotEmpty().WithMessage(ResourceMessagesException.DRIVERS_LICENSE_CATEGORY_EMPTY);
            When(user => !string.IsNullOrEmpty(user.DriversLicenseCategory), () =>
            {
                RuleFor(user => user.DriversLicenseCategory)
                .Custom((x, context) =>
                {
                    if (!x.Equals("A") && !x.Equals("B") && !x.Equals("AB"))
                        context.AddFailure(ResourceMessagesException.DRIVERS_LICENSE_CATEGORY_INVALID);
                });
            });
        }
    }
}
