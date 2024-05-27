using System.Net.Http.Json;

namespace WebApi.Test
{
    public class CustomClassFixture: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public CustomClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "en-US")
        {
            ChangeRequestCulture(culture);
            return await _httpClient.PostAsJsonAsync(method, request);
        }

        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }

        protected async Task<HttpResponseMessage> DoGet(string method, string token = "", string culture = "en-US")
        {
            AuthorizeRequest(token);
            ChangeRequestCulture(culture);
            return await _httpClient.GetAsync(method);
        }

        private void AuthorizeRequest(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return;

            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }

        protected async Task<HttpResponseMessage> DoPut(string method, object request, string token = "", string culture = "en-US")
        {
            AuthorizeRequest(token);
            ChangeRequestCulture(culture);
            return await _httpClient.PutAsJsonAsync(method, request);
        }
    }
}
