﻿using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.User.Register;
using rent.exceptions;

namespace Validators.Test.User.Register
{

    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.com";
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Error_NRLE_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.NRLE = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.NRLE_EMPTY));
        }

        [Fact]
        public void Error_DriversLicense_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.DriversLicense = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.DRIVERS_LICENSE_EMPTY));
        }

        [Fact]
        public void Error_DriversLicenseCategory_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.DriversLicenseCategory = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.DRIVERS_LICENSE_CATEGORY_EMPTY));
        }

        [Fact]
        public void Error_DriversLicenseCategory_Invalid()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.DriversLicenseCategory = "X423$";
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(x => x.ErrorMessage.Equals(ResourceMessagesException.DRIVERS_LICENSE_CATEGORY_INVALID));
        }
    }
}
