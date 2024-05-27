using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using rent.user.api.Filters;
using rent.user.communication.Responses;
using rent.user.domain.Repositories.User;
using rent.user.domain.Security.Tokens;
using rent.user.exceptions;
using rent.user.exceptions.ExceptionsBase;

namespace rent.user.api.Attributes
{
    public class AuthenticateUserAttribute : TypeFilterAttribute
    {
        public AuthenticateUserAttribute() : base(typeof(AuthenticateUserFilter))
        {
        }
    }
}
