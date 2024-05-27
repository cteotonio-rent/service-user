using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.user.application.UseCases.Login.DoLogin;
using rent.user.communication.Requests;
using rent.user.exceptions;
using rent.user.exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateDoLoginUseCase(user);

            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = password
            });

            result.Should().NotBeNull();
            result.Tokens.Should().NotBeNull();
            result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
            result.Tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateDoLoginUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(e => e.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
        }

        private static DoLoginUseCase CreateDoLoginUseCase(rent.user.domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

            if (user != null)
                userReadOnlyRepository.GetByEmailAndPassword(user);

            return new DoLoginUseCase(userReadOnlyRepository.Build(), accessTokenGenerator, passwordEncripter);
        }
    }
}
