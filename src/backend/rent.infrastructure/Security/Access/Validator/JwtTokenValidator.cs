using Microsoft.IdentityModel.Tokens;
using rent.domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace rent.infrastructure.Security.Access.Validator
{
    public class JwtTokenValidator: JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _singningKey;

        public JwtTokenValidator(string singningKey) => _singningKey = singningKey;

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false,
                 IssuerSigningKey = SecurityKey(_singningKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            var userIdentifier =  principal.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
            return Guid.Parse(userIdentifier);
        }
    }
}
