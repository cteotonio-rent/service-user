using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using CommomTestUtilities.Image;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Test.User.Update
{
    [Trait("IntegrationTest", "UserDriversLicenceImageEndPoint")]
    public class UpdateUserImageTest : CustomClassFixture
    {
        private const string _url = "user/image";
        private readonly Guid _userIdentifier;
        public UpdateUserImageTest(CustomWebApplicationFactory factory) : base(factory)
        {
            _userIdentifier = factory.GetUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var drivresLicenseImage = DriversLicencseImageBuilder.Build();


            var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
            //// Act
            //var response = await DoPut(_url, formData, token);
            //// Assert
            //Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);



            var file = System.IO.File.OpenRead("C:\\Users\\Cleber\\Downloads\\profile_thumb.jpg");
            HttpContent fileStreamContent = new StreamContent(file);

            var formData = new MultipartFormDataContent
        {
            { fileStreamContent, "file", "profile_thumb.jpg" }
            };

            var response = await DoPut(_url, formData, token);

            fileStreamContent.Dispose();
            formData.Dispose();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }
    }
}
