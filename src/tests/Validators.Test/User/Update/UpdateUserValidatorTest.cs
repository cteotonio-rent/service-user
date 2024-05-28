using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.application.UseCases.User.Update;
using rent.exceptions;

namespace Validators.Test.User.Update
{
    public class UpdateUserValidatorTest
    {
        [Fact]
        public async Task Success()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();
            var result = validator.Validate(request);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Error_Email_Invalid()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();

            request.Email = "InvalidEmail";
            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesException.EMAIL_INVALID);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();

            request.Name = string.Empty;
            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesException.NAME_EMPTY);
        }

        [Fact]
        public async Task Error_Email_Empty()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserJsonBuilder.Build();

            request.Email = string.Empty;
            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesException.EMAIL_EMPTY);
        }
    }
}
