using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;

namespace WebApi.Test.User.Update
{
    public class UpdateUserInvalidTokenTest: CustomClassFixture
    {
        private const string _url = "user";
        public UpdateUserInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Error_Invalid_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var response = await DoPut(_url, request, "invalid_token");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Empty_Token()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var response = await DoPut(_url, request, "");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
            var response = await DoPut(_url, request, token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    
    }
}
