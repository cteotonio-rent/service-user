using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.Motorcycle.Register;
using rent.application.UseCases.User.Register;
using rent.exceptions;

namespace Validators.Test.Motorcycle.Register
{
    public  class RegisterMotorcycleValidateTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Model_Empty()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Model = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.MODEL_EMPTY));
        }

        [Fact]
        public void Error_Year_Empty()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Year = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.YEAR_EMPTY));
        }

        [Fact]
        public void Error_Year_Qtde_Characters_Invalid()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Year = "202";
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.YEAR_QTDE_CHARACTERES_INVALID));
        }

        [Fact]
        public void Error_Year_Invalid_Is_Not_Number()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Year = "AAAA";
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.YEAR_INVALID_IS_NOT_NUMBER));
        }

        [Fact]
        public void Error_LicensePlate_Empty()
        {
            var validator = new RegisterMotorcycleValidator();
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.LicensePlate = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.LICENSE_PLATE_EMPTY));
        }
    }
}
