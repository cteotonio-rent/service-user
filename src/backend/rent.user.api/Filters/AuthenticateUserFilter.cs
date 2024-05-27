using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using rent.user.communication.Responses;
using rent.user.domain.Repositories.User;
using rent.user.domain.Security.Tokens;
using rent.user.exceptions;
using rent.user.exceptions.ExceptionsBase;

namespace rent.user.api.Filters
{
    public class AuthenticateUserFilter: IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public AuthenticateUserFilter(
            IAccessTokenValidator accessTokenValidator,
            IUserReadOnlyRepository userReadOnlyRepository)
        {
            _accessTokenValidator = accessTokenValidator;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);
                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);
                var exist = await _userReadOnlyRepository.ExistsActiveUserWithIdentifier(userIdentifier);

                if (!exist.HasValue || !exist.Value)
                {
                    throw new UserException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
                }
            }
            catch (UserException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch (SecurityTokenExpiredException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message) { TokenIsExpired = true });
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authentication))
            {
                throw new UserException(ResourceMessagesException.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
