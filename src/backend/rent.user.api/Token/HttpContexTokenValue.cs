using rent.user.domain.Security.Tokens;

namespace rent.user.api.Token
{
    public class HttpContexTokenValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContexTokenValue(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Value()
        {
           var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
            return authentication["Bearer ".Length..].Trim();
        }
    }
}
