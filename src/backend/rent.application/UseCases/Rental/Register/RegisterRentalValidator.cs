using FluentValidation;
using rent.communication.Requests;
using rent.exceptions;

namespace rent.application.UseCases.Rental.Register
{
    public class RegisterRentalValidator : AbstractValidator<RequestRegisterRentalJson>
    {
        public RegisterRentalValidator()
        {
            RuleFor(x => x.RealEndDate).GreaterThan(System.DateTime.Now.AddDays(1)).WithMessage(ResourceMessagesException.REAL_END_DATE_INVALID);
            RuleFor(x => x.RentalPlanDays).GreaterThanOrEqualTo(1).WithMessage(ResourceMessagesException.RENTAL_PLAN_DAYS_INVALID);
            When(rental => rental.RentalPlanDays > 0, () =>
            {
                RuleFor(rental => rental.RentalPlanDays)
                .Custom((x, context) =>
                {
                    if (!x.Equals(7) && !x.Equals(15) && !x.Equals(30))
                        context.AddFailure(ResourceMessagesException.RENTAL_PLAN_DAYS_NOT_ALLOWED);
                });
            });
        }
    }
}
