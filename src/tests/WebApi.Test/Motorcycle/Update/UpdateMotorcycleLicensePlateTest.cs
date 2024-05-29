using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using rent.exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.InlineData;

namespace WebApi.Test.Motorcycle.Update
{
    public class UpdateMotorcycleLicensePlateTest : CustomClassFixture
    {
        private readonly string _url = "motorcycle";
        private readonly Guid _userIdentifier;
        private readonly rent.domain.Entities.Motorcycle _motorcycle;
        public UpdateMotorcycleLicensePlateTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
            _motorcycle = factory.GetMotorcycle();
        }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut(_url + $"/{_motorcycle._id}", request, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Model(string culture)
        {
            var request = RequestUpdateMotorcycleLicensePlateJsonBuilder.Build();
            request.LicensePlate = string.Empty;
            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

            var response = await DoPut(_url + $"/{_motorcycle._id}", request, culture: culture, token: token);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString("LICENSE_PLATE_EMPTY", new CultureInfo(culture));

            erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
