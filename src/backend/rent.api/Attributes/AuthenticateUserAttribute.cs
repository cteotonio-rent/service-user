using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using rent.api.Filters;
using rent.communication.Responses;
using rent.domain.Repositories.User;
using rent.domain.Security.Tokens;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace rent.api.Attributes
{
    public class AuthenticateUserAttribute : TypeFilterAttribute
    {
        public AuthenticateUserAttribute() : base(typeof(AuthenticateUserFilter))
        {
        }
    }
}
