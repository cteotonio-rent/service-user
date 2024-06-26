﻿using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using rent.application.UseCases.Login.DoLogin;
using rent.communication.Requests;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

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

        private static DoLoginUseCase CreateDoLoginUseCase(rent.domain.Entities.User? user = null)
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
