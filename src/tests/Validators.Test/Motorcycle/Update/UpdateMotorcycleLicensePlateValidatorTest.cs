using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.Motorcycle.Update;
using rent.exceptions;

namespace Validators.Test.Motorcycle.Update
{
    public class UpdateMotorcycleLicensePlateValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateMotorcycleLicensePlateValidator();
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_LicensePlate_Empty()
        {
            var validator = new UpdateMotorcycleLicensePlateValidator();
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();
            request.LicensePlate = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.LICENSE_PLATE_EMPTY));
        }
    }
}
