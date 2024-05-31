using Microsoft.EntityFrameworkCore;
using rent.domain.Entities;
using rent.domain.Enuns;
using rent.domain.Security.Tokens;
using rent.domain.Services.LoggedUser;
using rent.infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace rent.infrastructure.Services.LoggedUser
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

        public async Task<bool> IsAuthorized (List<UserType> userTypeList)
        {
            var user = await User();
            return userTypeList.Any(ut => ut == user.UserType);
        }
    }
}
