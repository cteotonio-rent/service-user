using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rent.communication.Responses;
using rent.exceptions;
using System.Net;

namespace rent.api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is rent.exceptions.ExceptionsBase.UserException)
                HandleUserException(context);
            else
                ThrowUnknowException(context);

            context.ExceptionHandled = true;
        }

        private void HandleUserException(ExceptionContext context)
        {
            if (context.Exception is rent.exceptions.ExceptionsBase.ErrorOnValidationException)
            {
                var exception = context.Exception as rent.exceptions.ExceptionsBase.ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }
            else if (context.Exception is rent.exceptions.ExceptionsBase.InvalidLoginException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else if (context.Exception is rent.exceptions.ExceptionsBase.InvalidFileTypeException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
