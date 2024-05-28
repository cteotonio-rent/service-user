using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Motorcycle.Get
{
    public class GetMotorcycleTest : CustomClassFixture
    {
        private readonly string _url = "motorcycle";
        private readonly Guid _userIdentifier;
        private readonly rent.domain.Entities.Motorcycle _motorcycle;
        public GetMotorcycleTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _motorcycle = factory.GetMotorcycle();
        }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterMotorcycleJsonBuilder.Build();
            request.LicensePlate = _motorcycle.LicensePlate;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoGet(_url + "/getbylicenseplate?licensePlate=" + _motorcycle.LicensePlate, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("licensePlate").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.LicensePlate);
        }

        [Fact]
        public async Task No_Content()
        {
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoGet(_url + "/getbylicenseplate?licensePlate=vfsvas", token: token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Empty_Token()
        {
            var response = await DoGet(_url + "/getbylicenseplate?licensePlate=vfsvas", token: "");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }
    }
}
