using CommomTestUtilities.Requests;
using FluentAssertions;
using rent.user.exceptions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : CustomClassFixture
    {
        private readonly string _url = "user";
        public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var response = await DoPost(_url, request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
            responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should().NotBeNullOrWhiteSpace();
        }


        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var response = await DoPost(_url, request, culture);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

            erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
