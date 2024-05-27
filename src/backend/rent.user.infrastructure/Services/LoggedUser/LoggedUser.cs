using Microsoft.EntityFrameworkCore;
using rent.user.domain.Entities;
using rent.user.domain.Security.Tokens;
using rent.user.domain.Services.LoggedUser;
using rent.user.infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace rent.user.infrastructure.Services.LoggedUser
{
    public class LoggedUser: ILoggedUser
    {
        private readonly UserDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(UserDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider; 
        }

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtScurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtScurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;

            var userIdentifier = Guid.Parse(identifier);
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserUniqueIdentifier.Equals(userIdentifier));
            
            return user;
        }
    }
}
