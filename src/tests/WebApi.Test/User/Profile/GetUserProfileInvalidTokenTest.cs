using CommomTestUtilities.Token;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.User.Profile
{
    public class GetUserProfileInvalidTokenTest: CustomClassFixture
    {
        private readonly string _url = "user";

        public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Error_Invalid_Token()
        {
            var response = await DoGet(_url, token: "invalid");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var response = await DoGet(_url, token: string.Empty);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
            var response = await DoGet(_url, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

    }
}
