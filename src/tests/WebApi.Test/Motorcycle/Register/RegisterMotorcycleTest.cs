using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using rent.exceptions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Motorcycle.Register
{
    public class RegisterMotorcycleTest : CustomClassFixture
    {
        private readonly string _url = "motorcycle";
        private readonly Guid _userIdentifier;
        public RegisterMotorcycleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(_url, request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("licensePlate").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.LicensePlate);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Model(string culture)
        {
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.Model = string.Empty;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPost(_url, request, culture: culture, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("MODEL_EMPTY", new CultureInfo(culture));

            erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Token(string culture)
        {
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            
            var response = await DoPost(_url, request, culture: culture, token: "");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }
    }
}
