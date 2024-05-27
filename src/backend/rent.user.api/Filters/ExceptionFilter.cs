using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rent.user.communication.Responses;
using rent.user.exceptions;
using System.Net;

namespace rent.user.api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is rent.user.exceptions.ExceptionsBase.UserException)
                HandleUserException(context);
            else
                ThrowUnknowException(context);

            context.ExceptionHandled = true;
        }

        private void HandleUserException(ExceptionContext context)
        {
            if (context.Exception is rent.user.exceptions.ExceptionsBase.ErrorOnValidationException)
            {
                var exception = context.Exception as rent.user.exceptions.ExceptionsBase.ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }
            else if (context.Exception is rent.user.exceptions.ExceptionsBase.InvalidLoginException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));

        }
    }
}
