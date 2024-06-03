using CommomTestUtilities.Entities;
using CommomTestUtilities.LoggedUser;
using CommomTestUtilities.Mapper;
using FluentAssertions;
using rent.application.UseCases.User.Profile;

namespace UseCases.Test.User.Profile
{
    public class GetUserProfileUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            (var user, var _) = UserBuilder.Build();

            var useCase = CreateUseCase(user); 

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
        }

        private static GetUserProfileUseCase CreateUseCase(rent.domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = new LoggedUserBuilder().IsAuthorized(user).Build(user);

            return new GetUserProfileUseCase(loggedUser, mapper);
        }
    }
}
